namespace IntraVision.Web.Mvc.Controls
{
    /*public class GridView<TEntity> : Grid<TEntity>
        where TEntity: class
    {
        public string Render(HtmlHelper html)
        {
            if (_Options == null) _Options = new GridOptionsOld();

            var model = CreateGridModel(html);

            _Query = _QuerySorter != null ? _QuerySorter(_Query, _Options) : Order(_Query, _Options);
            _Query = _QuerySearcher != null ? _QuerySearcher(_Query, model, _Options.SearchString) : Search(_Query, model, _Options.SearchString);

            var data = _Query.AsPagination(_Options.Page, _Options.PageSize);

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

            new Grid<TEntity>(data, sw, html.ViewContext).WithModel(model).Sort(_Options.SortOptions).Render();
            sb.Append(new Pager(data, html));
            AppendSearchString(sb);
            AppendGridOptions(sb, model);

            return sb.ToString();
        }
    }*/
}
