using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using IntraVision.Web.Mvc.Route;
using System.Collections.Generic;
using Microsoft.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static string Jsonify<T>(this HtmlHelper html, T data)
            where T:class
        {
            return ServiceStack.Text.JsonSerializer.SerializeToString(data);
        }

        public static string ValueCheckbox(this HtmlHelper html, string name, object value, bool selected)
        {
            return ValueCheckbox(html, name, value, selected, null);
        }

        public static string ValueCheckbox(this HtmlHelper html, string name, object value, bool selected, object attributes)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "checkbox");
            tagBuilder.MergeAttribute("id", String.Format("{0}{1}", name, value));
            tagBuilder.MergeAttribute("name", name);
            tagBuilder.MergeAttribute("value", value.ToString());
            if (selected) tagBuilder.MergeAttribute("checked", "checked");
            tagBuilder.MergeAttributes(new RouteValueDictionary(attributes));
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        public static string ImageActionLink(this HtmlHelper html, string imageUrl, string actionName)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            var link = html.ActionLink("[replaceme]", actionName).ToHtmlString();
            return link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }

        public static string ImageActionLink(this HtmlHelper html, string imageUrl, string actionName, string controller, object linkRouteValues, object imageAttributes, object linkAttributes)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttributes(new RouteValueDictionary(imageAttributes));
            var link = html.ActionLink("[replaceme]", actionName, controller, linkRouteValues, linkAttributes).ToHtmlString();
            return link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }

        public static string ImageActionLink<TController>(this HtmlHelper html, Expression<Action<TController>> action, string imageUrl) where TController : Controller
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            var link = html.ActionLink(action, "[replaceme]").ToHtmlString();
            return link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }

        public static string ImageActionLink<TController>(this HtmlHelper html, Expression<Action<TController>> action, string imageUrl, object imageAttributes, object linkAttributes) where TController : Controller
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttributes(new RouteValueDictionary(imageAttributes));
            var link = html.ActionLink(action, "[replaceme]", linkAttributes).ToHtmlString();
            return link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }

        public static string ActionLinkWithImage<TController>(this HtmlHelper html, Expression<Action<TController>> action, string imgSrc) where TController : Controller
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            string imgUrl = urlHelper.Content(imgSrc);
            var imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", imgUrl);
            string img = imgTagBuilder.ToString(TagRenderMode.Normal);
            var tagBuilder = new TagBuilder("a")
            {
                InnerHtml = img
            };
            tagBuilder.MergeAttribute("href", LinkBuilder.BuildUrlFromExpression(html.ViewContext.RequestContext, html.RouteCollection, action));
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        public static string ActionLinkWithImage<TController>(this HtmlHelper html, Expression<Action<TController>> action, string imgSrc, object imageAttributes, object linkAttributes) where TController : Controller
        {
            return ActionLinkWithImage(html, action, imgSrc, "", imageAttributes, linkAttributes);
        }

        public static string ActionLinkWithImage<TController>(this HtmlHelper html, Expression<Action<TController>> action, string imgSrc, string linkText, object imageAttributes, object linkAttributes) where TController : Controller
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            string imgUrl = urlHelper.Content(imgSrc);
            var imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", imgUrl);

            imgTagBuilder.MergeAttributes(new RouteValueDictionary(imageAttributes));

            string img = imgTagBuilder.ToString(TagRenderMode.Normal);

            var tagBuilder = new TagBuilder("a")
                                        {
                                            InnerHtml = img + linkText
                                        };

            tagBuilder.MergeAttributes(new RouteValueDictionary(linkAttributes));
            tagBuilder.MergeAttribute("href", LinkBuilder.BuildUrlFromExpression(html.ViewContext.RequestContext, html.RouteCollection, action));
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        public static string ActionLinkWithSpan<TController>(this HtmlHelper html, Expression<Action<TController>> action, string linkText, object linkAttributes) where TController : Controller
        {
            var span = new TagBuilder("span");
            span.AddCssClass("icon");
            var tagBuilder = new TagBuilder("a")
            {
                InnerHtml = span.ToString(TagRenderMode.SelfClosing) + linkText
            };

            tagBuilder.MergeAttributes(new RouteValueDictionary(linkAttributes));
            tagBuilder.MergeAttribute("href", LinkBuilder.BuildUrlFromExpression(html.ViewContext.RequestContext, html.RouteCollection, action));
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        public static string DeleteLink<TController>(this HtmlHelper html, Expression<Action<TController>> action) where TController : Controller
        {
            return ActionLinkWithSpan(html, action, "", new { @class = "delete" });
        }

        public static void UpdatePanel<TController>(this HtmlHelper html, Expression<Action<TController>> action)
            where TController : Controller
        {
            UpdatePanel<TController>(html, action, null);
        }

        public static void UpdatePanel<TController>(this HtmlHelper html, Expression<Action<TController>> action, object htmlAttributes)
            where TController : Controller
        {
            UpdatePanel<TController>(html, action, new RouteValueDictionary(htmlAttributes));
        }

        public static void UpdatePanel<TController>(this HtmlHelper html, Expression<Action<TController>> action, IDictionary<string, object> htmlAttributes)
            where TController : Controller
        {
            var rv = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(action);
            var rv2 = new RouteValueDictionary(rv.Where(r => r.Value != null && (r.Value.GetType().IsPrimitive || r.Value.GetType() == typeof(string))).ToDictionary(r => r.Key, r => r.Value));
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var div = new TagBuilder("div");
            div.MergeAttribute("data-action", url.Action(rv2["action"].ToString(), rv2));
            div.MergeAttributes(htmlAttributes, true);
            div.AddCssClass("updatepanel");

            html.ViewContext.Writer.WriteLine(div.ToString(TagRenderMode.StartTag));
            html.RenderAction<TController>(action);
            html.ViewContext.Writer.WriteLine(div.ToString(TagRenderMode.EndTag));
        }

        public static void UpdatePanel(this HtmlHelper html, string action)
        {
            UpdatePanel(html, action, null, null, null);
        }

        public static void UpdatePanel(this HtmlHelper html, string action, IDictionary<string, object> htmlAttributes)
        {
            UpdatePanel(html, action, null, null, htmlAttributes);
        }

        public static void UpdatePanel(this HtmlHelper html, string action, object htmlAttributes)
        {
            UpdatePanel(html, action, null, null, new RouteValueDictionary(htmlAttributes));
        }

        public static void UpdatePanel(this HtmlHelper html, string action, string controller, object htmlAttributes)
        {
            UpdatePanel(html, action, controller, null, new RouteValueDictionary(htmlAttributes));
        }

        public static void UpdatePanel(this HtmlHelper html, string action, string controller, object routeValues, object htmlAttributes)
        {
            UpdatePanel(html, action, controller, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static void UpdatePanel(this HtmlHelper html, string routeName, string action, string controller, object routeValues, object htmlAttributes)
        {
            UpdatePanel(html, routeName, action, controller, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static void UpdatePanel(this HtmlHelper html, string action, string controller, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var div = new TagBuilder("div");
            div.MergeAttribute("data-action", url.Action(action, controller, routeValues));
            div.MergeAttributes(htmlAttributes, true);
            div.AddCssClass("updatepanel");

            html.ViewContext.Writer.WriteLine(div.ToString(TagRenderMode.StartTag));
            html.RenderAction(action, controller, routeValues);
            html.ViewContext.Writer.WriteLine(div.ToString(TagRenderMode.EndTag));
        }

        public static void UpdatePanel(this HtmlHelper html, string routeName, string action, string controller, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var div = new TagBuilder("div");
            routeValues["action"] = action;
            routeValues["controller"] = controller;
            div.MergeAttribute("data-action", url.RouteUrl(routeName, routeValues));
            div.MergeAttributes(htmlAttributes, true);
            div.AddCssClass("updatepanel");

            html.ViewContext.Writer.WriteLine(div.ToString(TagRenderMode.StartTag));
            html.RenderAction(action, controller, routeValues);
            html.ViewContext.Writer.WriteLine(div.ToString(TagRenderMode.EndTag));
        }

        public static MvcHtmlString EditorWithLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return MvcHtmlString.Create(html.LabelFor(expression).ToHtmlString() + html.EditorFor(expression).ToHtmlString());
        }

        public static MvcHtmlString EditorWithLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
        {
            return MvcHtmlString.Create(html.LabelFor(expression).ToHtmlString() + html.EditorFor(expression, additionalViewData).ToHtmlString());
        }

        public static MvcHtmlString EditorWithLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
        {
            return MvcHtmlString.Create(html.LabelFor(expression).ToHtmlString() + html.EditorFor(expression, templateName).ToHtmlString());
        }

        public static MvcHtmlString EditorWithLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData)
        {
            return MvcHtmlString.Create(html.LabelFor(expression).ToHtmlString() + html.EditorFor(expression, templateName, additionalViewData).ToHtmlString());
        }

        public static MvcHtmlString EditorWithLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName)
        {
            return MvcHtmlString.Create(html.LabelFor(expression).ToHtmlString() + html.EditorFor(expression, templateName, htmlFieldName).ToHtmlString());
        }

        public static MvcHtmlString EditorWithLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            return MvcHtmlString.Create(html.LabelFor(expression).ToHtmlString() + html.EditorFor(expression, templateName, htmlFieldName, additionalViewData).ToHtmlString());
        }

        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, bool requireAbsoluteUrl)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), requireAbsoluteUrl);
        }

        public static string ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool requireAbsoluteUrl)
        {
            if (requireAbsoluteUrl)
            {
                HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
                RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);

                routeData.Values["controller"] = controllerName;
                routeData.Values["action"] = actionName;

                DomainRoute domainRoute = routeData.Route as DomainRoute;
                if (domainRoute != null)
                {
                    DomainData domainData = domainRoute.GetDomainData(new RequestContext(currentContext, routeData), routeData.Values);
                    return htmlHelper.ActionLink(linkText, actionName, controllerName, domainData.Protocol, domainData.HostName, domainData.Fragment, routeData.Values, null).ToHtmlString();
                }
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString();
        }

        public static HtmlString ToHtmlAttributeString(this Object attributes)
        {
            var props = attributes.GetType().GetProperties();
            var pairs = props.Select(x => string.Format(@"{0}=""{1}""", x.Name, x.GetValue(attributes, null))).ToArray();
            return new HtmlString(string.Join(" ", pairs));
        }
    }
}
