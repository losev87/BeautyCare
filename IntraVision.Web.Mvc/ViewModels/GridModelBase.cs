using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Autofac;
using IntraVision.Data;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc
{
    public class GridModelBase<TEntity> : GridModel<TEntity>
        where TEntity : class, INamedEntityBase
    {
        public GridModelBase(HtmlHelper html)
        {
            var tEntityType = typeof (TEntity);

            Column.For(m => html.DeleteLink(m.Id)).Attributes(@class => "options").DoNotEncode();

            foreach (var column in tEntityType
                                    .GetProperties()
                                    .Select(p => new { property = p, attribute = p.GetCustomAttributes(false).OfType<GridColumnAttribute>().FirstOrDefault() })
                                    .Where(a => a.attribute != null).OrderBy(a => a.attribute.Order))
            {
                var displayAttribute = column.property.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
                var named = !string.IsNullOrEmpty(column.attribute.Named) ?
                                    column.attribute.Named : (displayAttribute != null ? displayAttribute.Name : column.property.Name);
                var property = !string.IsNullOrEmpty(column.attribute.Property) ?
                                    column.attribute.Property : column.property.Name;
                var sortable = !string.IsNullOrEmpty(column.attribute.Sortable) ?
                                    column.attribute.Sortable : (!string.IsNullOrEmpty(property) ? property : column.property.Name);

                if (!string.IsNullOrEmpty(column.attribute.ActionEditLink))
                    Column.For(m => html.EditLink(m.Id, m.Name, column.attribute.ActionEditLink, new[] { column.attribute.Css })).DoNotEncode().Sortable(sortable).Named(named);
                else if (column.attribute.EditLink)
                    Column.For(m => html.EditLink(m.Id, new[] { "dialog-form" })).Sortable(sortable).DoNotEncode();
                else
                {
                    string[] props = property.Split('.');
                    Type type = tEntityType;
                    ParameterExpression arg = Expression.Parameter(type, "x");
                    Expression expr = arg;
                    foreach (string prop in props)
                    {
                        // use reflection (not ComponentModel) to mirror LINQ
                        var pi = type.GetProperty(prop);
                        expr = Expression.Property(expr, pi);
                        type = pi.PropertyType;
                    }

                    if (expr.Type.IsValueType)
                        expr = Expression.Convert(expr, typeof (object));

                    Expression<Func<TEntity, object>> lambda = Expression.Lambda<Func<TEntity, object>>(expr, arg);

                    var col = Column.For(lambda).Sortable(sortable).Named(named);

                    if (column.attribute.DoNotEncode)
                        col.DoNotEncode();
                }
            }

            if (tEntityType.IsAssignableTo<ISortableEntity>())
            {
                Sections.RowAttributes(r => new Dictionary<string, object> { { "id", "row_" + r.Item.Id } });

                var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                Attributes(new Dictionary<string, object> {{"sortable", urlHelper.Action("Sortable", tEntityType.Name)}});
            }
        }

        public GridModelBase()
        {
            var tEntityType = typeof(TEntity);

            foreach (var column in tEntityType
                                    .GetProperties()
                                    .Select(p => new { property = p, attribute = p.GetCustomAttributes(false).OfType<GridColumnAttribute>().FirstOrDefault() })
                                    .Where(a => a.attribute != null).OrderBy(a => a.attribute.Order))
            {
                var displayAttribute = column.property.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
                var named = !string.IsNullOrEmpty(column.attribute.Named) ? column.attribute.Named : displayAttribute != null ? displayAttribute.Name : column.property.Name;

                string[] props = column.attribute.Property.Split('.');
                Type type = tEntityType;
                ParameterExpression arg = Expression.Parameter(type, "x");
                Expression expr = arg;
                foreach (string prop in props)
                {
                    // use reflection (not ComponentModel) to mirror LINQ
                    var pi = type.GetProperty(prop);
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }

                Expression<Func<TEntity, object>> lambda = Expression.Lambda<Func<TEntity, object>>(expr, arg);

                var col = Column.For(lambda).Named(named);

                if (column.attribute.DoNotEncode)
                    col.DoNotEncode();
            }
        }
    }
}
