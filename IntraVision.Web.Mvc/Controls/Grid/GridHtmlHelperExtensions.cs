using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Linq.Expressions;
using IntraVision.Web.Mvc.Controls.Syntax;
using Microsoft.Web.Mvc;

namespace IntraVision.Web.Mvc.Controls
{
    public static class GridHtmlHelperExtensions
    {
        /// <summary>
        /// Создает ссылку для удаления элемента (с иконкой мусорного ведра)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string DeleteLink(this HtmlHelper html, long id)
        {
            var aTag = new TagBuilder("a");
            var url = new UrlHelper(html.ViewContext.RequestContext);
            aTag.MergeAttribute("href", url.Action("Delete", new { id }));
            aTag.MergeAttribute("id", "del_" + id);
            aTag.AddCssClass("delete");
            return aTag.ToString(TagRenderMode.Normal);
        }

        public static string DeleteLink(this HtmlHelper html, object route)
        {
            var aTag = new TagBuilder("a");
            var url = new UrlHelper(html.ViewContext.RequestContext);
            aTag.MergeAttribute("href", url.Action("Delete", route));
            aTag.AddCssClass("delete");
            return aTag.ToString(TagRenderMode.Normal);
        }
        
        /// <summary>
        /// Создает ссылку для удаления элемента с токеном безопасности против CSRF атак (с иконкой мусорного ведра)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string DeleteLinkWithAntiForgery(this HtmlHelper html, long id)
        {
            return AntiForgery.GetHtml() + DeleteLink(html, id);
        }

        /// <summary>
        /// Создает ссылку для редактирования элемента (с иконкой)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string EditLink(this HtmlHelper html, long id, IEnumerable<string> cssClass)
        {
            var aTag = new TagBuilder("a");
            var url = new UrlHelper(html.ViewContext.RequestContext);
            aTag.MergeAttribute("href", url.Action("Edit", new { id = id }));
            aTag.MergeAttribute("id", "edit_" + id);
            aTag.AddCssClass("edit");

            foreach (var css in cssClass)
                aTag.AddCssClass(css);

            return aTag.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Создает ссылку для редактирования элемента (с текстом)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string EditLink(this HtmlHelper html, object id, string text, IEnumerable<string> cssClass)
        {
            var aTag = new TagBuilder("a");
            var url = new UrlHelper(html.ViewContext.RequestContext);
            aTag.MergeAttribute("href", url.Action("Edit", new { id = id }));
            aTag.MergeAttribute("id", "edit_" + id);

            aTag.SetInnerText(text);

            foreach (var css in cssClass)
                aTag.AddCssClass(css);

            return aTag.ToString(TagRenderMode.Normal);
        }

        public static string EditLink(this HtmlHelper html, long id, string text, string action, IEnumerable<string> cssClass)
        {
            var aTag = new TagBuilder("a");
            var url = new UrlHelper(html.ViewContext.RequestContext);
            aTag.MergeAttribute("href", url.Action(action, new { id = id }));
            aTag.MergeAttribute("id", "edit_" + id);

            aTag.SetInnerText(text);

            foreach (var css in cssClass)
                aTag.AddCssClass(css);

            return aTag.ToString(TagRenderMode.Normal);
        }

        public static string EditLink(this HtmlHelper html, object route, string text, string action, IEnumerable<string> cssClass)
        {
            var aTag = new TagBuilder("a");
            var url = new UrlHelper(html.ViewContext.RequestContext);
            aTag.MergeAttribute("href", url.Action(action, route));

            aTag.SetInnerText(text);

            foreach (var css in cssClass)
                aTag.AddCssClass(css);

            return aTag.ToString(TagRenderMode.Normal);
        }

        public static string EditLink(this HtmlHelper html, long id, string text, string action, string controller, IEnumerable<string> cssClass)
        {
            var aTag = new TagBuilder("a");
            var url = new UrlHelper(html.ViewContext.RequestContext);
            aTag.MergeAttribute("href", url.Action(action, controller, new { id = id }));
            aTag.MergeAttribute("id", "edit_" + id);

            aTag.SetInnerText(text);

            foreach (var css in cssClass)
                aTag.AddCssClass(css);

            return aTag.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Создает картинку для перетаскивания строк в таблице
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string DragHandleImage(this HtmlHelper html)
        {
            var aTag = new TagBuilder("a");

            aTag.AddCssClass("move");

            return aTag.ToString(TagRenderMode.Normal);
        }

        public static string SearchString<TController>(this HtmlHelper html, Expression<Action<TController>> action, string updatePanelId)
            where TController: Controller
        {
            var div = new TagBuilder("div");
            div.AddCssClass("search");

            var form = new TagBuilder("form");
            form.MergeAttribute("action", LinkBuilder.BuildUrlFromExpression(html.ViewContext.RequestContext, html.RouteCollection, action));
            form.MergeAttribute("target", updatePanelId);
            form.AddCssClass("ajax-form");

            var input = new TagBuilder("input");
            input.MergeAttribute("name", "SearchString");


            var button = new TagBuilder("button");
            button.MergeAttribute("type", "submit");

            form.InnerHtml += input.ToString();
            form.InnerHtml += button.ToString();
            div.InnerHtml = form.ToString();

            return div.ToString();
        }

        public static IGrid<T> GridView<T>(this HtmlHelper helper, IEnumerable<T> dataSource) where T : class
        {
            return new Grid<T>(dataSource, helper.ViewContext.Writer, helper.ViewContext);
        }
    }
}
