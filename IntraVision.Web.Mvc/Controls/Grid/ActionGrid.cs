using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Principal;
using System.Text;
using Autofac;
using Autofac.Core;
using IntraVision.Core.Sorting;
using IntraVision.Core.Pagination;
using IntraVision.Web.Mvc.Autofac;
using System.Web.Mvc;
using System.IO;

namespace IntraVision.Web.Mvc.Controls
{
    public class ActionGrid<TEntity, TGridModel> : IActionGrid
        where TEntity : class
        where TGridModel : class, IGridModel<TEntity>
    {
        private IQueryable<TEntity> _Query;
        private IFilter<TEntity> _Filter;
        private IGridOptions _Options;
        //private Type _TModel;
        private Func<IQueryable<TEntity>, IGridOptions, IQueryable<TEntity>> _QuerySorter;
        private Func<IQueryable<TEntity>, IGridModel<TEntity>, string, IQueryable<TEntity>> _QuerySearcher;
        private IPrincipal _User;
        private bool _ShowSearchString = true;
        private bool _ShowGridOptionsString = true;
        private List<Parameter> _gridModelParameters;

        public ActionGrid(IQueryable<TEntity> query, IGridOptions options)
        {
            _Query = query;
            _Options = options;
            _ShowSearchString = options.ShowSearchString;
            _ShowGridOptionsString = options.ShowGridOptions;
        }

        public ActionGrid<TEntity, TGridModel> WithUser(IPrincipal user)
        {
            _User = user;
            return this;
        }

        public ActionGrid<TEntity, TGridModel> WithTGridModelParameters<TParameter>(params TParameter[] parameters)
        {
            if (_gridModelParameters == null)
                _gridModelParameters = new List<Parameter>();

            parameters.Aggregate(_gridModelParameters, (lp, tp) =>
                {
                    lp.Add(TypedParameter.From(tp));
                    return lp;
                });

            return this;
        }

        private IQueryable<TEntity> Order(IQueryable<TEntity> query, IGridOptions options)
        {
            if (query == null || options == null || options.SortOptions == null || !options.SortOptions.Any() || options.SortOptions.All(o => string.IsNullOrEmpty(o.Column)))
                return query;

            var orderBy = options.SortOptions.First();
            var orderedQueryable = query.OrderBy(orderBy.Column, orderBy.Direction);

            return options.SortOptions.Skip(1).Aggregate(orderedQueryable, (current, gridSortOptionse) => current.ThenBy(gridSortOptionse.Column, gridSortOptionse.Direction));
        }

        private IQueryable<TEntity> Search(IQueryable<TEntity> query, IGridModel<TEntity> model, string search)
        {
            search = search != null ? search.ToLower() : null;
            if (string.IsNullOrEmpty(search)) return query;
            var sb = new StringBuilder();

            //Ищем по подстроке
            foreach (var column in (from c in model.Columns where c.Searchable select c))
                sb.Append(sb.Length > 0 ? "||" : "").Append("(").Append(column.SearchName).Append("!= null").Append(" && ").Append(column.SearchName).Append(".ToLower().Contains(@0)").Append(")");

            int searchNumber = 0;
            if (Int32.TryParse(search, out searchNumber))
            {
                foreach (var column in (from c in model.Columns where c.NumericSearchable select c))
                    sb.Append(sb.Length > 0 ? "||" : "").Append(column.SearchName).Append(" == @1");
            }

            if (sb.Length > 0)
                return query.Where(sb.ToString(), search, searchNumber);
            return query;
        }

        public ActionGrid<TEntity, TGridModel> WithFilter(IFilter<TEntity> filter)
        {
            _Filter = filter;
            return this;
        }

        //public ActionGrid<TEntity, TGridModel> WithModelType(Type tmodel)
        //{
        //    return WithModelType(tmodel, null);
        //}

        //public ActionGrid<TEntity, TGridModel> WithModelType(Type tmodel, IMVCPrincipal user)
        //{
        //    if (!typeof(IGridModel<TEntity>).IsAssignableFrom(tmodel))
        //        throw new ArgumentException("Model type must implement IGridModel<T> interface!");

        //    _User = user;
        //    return this;
        //}

        public ActionGrid<TEntity, TGridModel> WithQuerySorter(Func<IQueryable<TEntity>, IGridOptions, IQueryable<TEntity>> sorter)
        {
            _QuerySorter = sorter;
            return this;
        }

        public ActionGrid<TEntity, TGridModel> WithQuerySearcher(Func<IQueryable<TEntity>, IGridModel<TEntity>, string, IQueryable<TEntity>> searcher)
        {
            _QuerySearcher = searcher;
            return this;
        }

        public string Render(HtmlHelper html)
        {
            if (_Options == null) _Options = new GridOptions();

            var model = CreateGridModel(html);

            _Query = _QuerySorter != null ? _QuerySorter(_Query, _Options) : Order(_Query, _Options);
            _Query = _QuerySearcher != null ? _QuerySearcher(_Query, model, _Options.SearchString) : Search(_Query, model, _Options.SearchString);

            var data = _Query.AsPagination(_Options.Page, _Options.PageSize, _Options.PagesOnPage);

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            if (_Options != null && _Options.VisibleColumns != null && _Options.VisibleColumns.Count > 0)
            {
                var visibleColumns = _Options.VisibleColumns;
                //Сначала скроем все колонки
                foreach (var column in model.Columns)
                {
                    //Возможная ошибка в конфигурации - пустое поле "Name" у колонки. Не будем ее скрывать.
                    if (string.IsNullOrEmpty(column.Name)) continue;

                    ((IGridColumn<TEntity>)column).Visible(false);
                }
                //Потом покажем видимые
                for (int i = 0; i < visibleColumns.Count; i++)
                {
                    var col = model.Columns.FirstOrDefault(c => c.Name == visibleColumns[i]);
                    if (col != null)
                    {
                        ((IGridColumn<TEntity>)col).Visible(true);
                        col.Order = i + 1;
                    }
                }
            }

            new Grid<TEntity>(data, sw, html.ViewContext).WithFilter(_Filter).WithModel(model).Sort(_Options.SortOptions).Render();
            if(_Options.ShowPager)
                sb.Append(new Pager(data, html));
            if (_ShowSearchString) AppendSearchString(sb);
            if (_ShowGridOptionsString) AppendGridOptions(sb, model);

            return sb.ToString();
        }

        public MemoryStream XlsxRender()
        {
            if (_Options == null) _Options = new GridOptions();

            //_Options.PageSize = Int32.MaxValue;

            var model = CreateGridModel();

            _Query = _QuerySorter != null ? _QuerySorter(_Query, _Options) : Order(_Query, _Options);
            _Query = _QuerySearcher != null ? _QuerySearcher(_Query, model, _Options.SearchString) : Search(_Query, model, _Options.SearchString);

            var data = _Query.AsPagination(_Options.Page, Int32.MaxValue, Int32.MaxValue);

            if (_Options != null && _Options.VisibleColumns != null && _Options.VisibleColumns.Count > 0)
            {
                var visibleColumns = _Options.VisibleColumns;
                //Сначала скроем все колонки
                foreach (var column in model.Columns)
                {
                    //Возможная ошибка в конфигурации - пустое поле "Name" у колонки. Не будем ее скрывать.
                    if (string.IsNullOrEmpty(column.Name)) continue;

                    ((IGridColumn<TEntity>)column).Visible(false);
                }
                //Потом покажем видимые
                for (int i = 0; i < visibleColumns.Count; i++)
                {
                    var col = model.Columns.FirstOrDefault(c => c.Name == visibleColumns[i]);
                    if (col != null)
                    {
                        ((IGridColumn<TEntity>)col).Visible(true);
                        col.Order = i + 1;
                    }
                }
            }

            return new Grid<TEntity>(data).WithModel(model).Sort(_Options.SortOptions).XlsxRender();
        }

        public MemoryStream XlsxRender(IXlsxGridRenderer<TEntity> xlsxGridRenderer)
        {
            if (_Options == null) _Options = new GridOptions();

            var model = CreateGridModel();

            _Query = _QuerySorter != null ? _QuerySorter(_Query, _Options) : Order(_Query, _Options);
            _Query = _QuerySearcher != null ? _QuerySearcher(_Query, model, _Options.SearchString) : Search(_Query, model, _Options.SearchString);

            var data = _Query.AsPagination(_Options.Page, _Options.PageSize, _Options.PagesOnPage);

            if (_Options != null && _Options.VisibleColumns != null && _Options.VisibleColumns.Count > 0)
            {
                var visibleColumns = _Options.VisibleColumns;
                //Сначала скроем все колонки
                foreach (var column in model.Columns)
                {
                    //Возможная ошибка в конфигурации - пустое поле "Name" у колонки. Не будем ее скрывать.
                    if (string.IsNullOrEmpty(column.Name)) continue;

                    ((IGridColumn<TEntity>)column).Visible(false);
                }
                //Потом покажем видимые
                for (int i = 0; i < visibleColumns.Count; i++)
                {
                    var col = model.Columns.FirstOrDefault(c => c.Name == visibleColumns[i]);
                    if (col != null)
                    {
                        ((IGridColumn<TEntity>)col).Visible(true);
                        col.Order = i + 1;
                    }
                }
            }

            return new Grid<TEntity>(data).WithModel(model).Sort(_Options.SortOptions).XlsxRenderUsing(xlsxGridRenderer).XlsxRender();
        }

        private void AppendSearchString(StringBuilder sb)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("search");

            var input = new TagBuilder("input");
            input.Attributes["name"] = "SearchString";
            input.Attributes["value"] = _Options.SearchString;

            var buttonClear = new TagBuilder("button");
            buttonClear.Attributes["type"]="button";
            buttonClear.AddCssClass("clear");

            var button = new TagBuilder("button");
            button.Attributes["type"] = "button";
            button.AddCssClass("search-button");

            div.InnerHtml += input.ToString();
            div.InnerHtml += button.ToString();
            div.InnerHtml += buttonClear.ToString();

            sb.Append(div.ToString());
        }

        private void AppendGridOptions(StringBuilder sb, IGridModel<TEntity> model)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("grid-options-button");

            var button = new TagBuilder("button");
            button.MergeAttribute("title", "Настроить видимость колонок");
            div.InnerHtml = button.ToString();
            sb.Append(div.ToString());

            var gridoptions = new TagBuilder("div");
            gridoptions.AddCssClass("grid-options");

            var form = new TagBuilder("form");
            var ulholder = new TagBuilder("div");
            ulholder.AddCssClass("checklist");

            var ul = new TagBuilder("ul");

            foreach (var col in model.Columns.OrderBy(c=>c.Order))
            {
                if (string.IsNullOrEmpty(col.Name)) continue;

                var li = new TagBuilder("li");
                var label = new TagBuilder("label");
                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "checkbox");
                cb.MergeAttribute("value", col.Name);
                cb.MergeAttribute("name", "VisibleColumns");

                if (col.Visible)
                    cb.MergeAttribute("checked", "checked");

                label.InnerHtml += cb.ToString();
                label.InnerHtml += col.DisplayName;

                li.InnerHtml = label.ToString();
                var span = new TagBuilder("span");
                span.AddCssClass("options");
                var a = new TagBuilder("a");
                a.AddCssClass("move");
                span.InnerHtml = a.ToString();
                li.InnerHtml += span.ToString();
                ul.InnerHtml += li.ToString();
            }
            ulholder.InnerHtml = ul.ToString();
            form.InnerHtml = ulholder.ToString();

            var formbottom = new TagBuilder("div");
            formbottom.AddCssClass("form-bottom");

            var apply = new TagBuilder("button");
            apply.SetInnerText("Применить");

            var cancel = new TagBuilder("a");
            cancel.AddCssClass("inline-button");
            cancel.AddCssClass("grid-options-hide");
            cancel.MergeAttribute("href", "javascript:;;");
            cancel.SetInnerText("Отмена");

            formbottom.InnerHtml = apply.ToString();
            formbottom.InnerHtml += cancel.ToString();

            gridoptions.InnerHtml = form.ToString();
            gridoptions.InnerHtml += formbottom.ToString();

            sb.Append(gridoptions.ToString());
        }

        public IGridModel<TEntity> CreateGridModel(HtmlHelper html)
        {
            WithTGridModelParameters(html);
            WithTGridModelParameters(_User);
            var tModel = GetInLifetimeScope.Instance<TGridModel>(_gridModelParameters.ToArray());

            if (tModel != null)
                return tModel;

            return new AutoColumnGridModel<TEntity>(new DataAnnotationsModelMetadataProvider());
        }

        public IGridModel<TEntity> CreateGridModel()
        {
            WithTGridModelParameters(_User);
            var tModel = GetInLifetimeScope.Instance<TGridModel>(_gridModelParameters.ToArray());

            if (tModel != null)
                return tModel;

            return new AutoColumnGridModel<TEntity>(new DataAnnotationsModelMetadataProvider());
        }
    }
}
