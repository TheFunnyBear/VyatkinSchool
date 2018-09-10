using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace VyatkinSchool.Helpers
{
    public static class MenuExtensions
    {
        public static IHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller,
            string icon
        )
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }

            var spanTagIcon = new TagBuilder("span");
            spanTagIcon.AddCssClass($"glyphicon {icon}");

            var spanTagText = new TagBuilder("span");
            spanTagText.SetInnerText(text);

            var linkTag = new TagBuilder("a");
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = urlHelper.Action(controller, action);
            linkTag.MergeAttribute("href", url);
            linkTag.InnerHtml = $"{text}{spanTagIcon.ToString()}";
            li.InnerHtml = linkTag.ToString();

            return new HtmlString(li.ToString());
        }

        public static IHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller
        )
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }

            li.InnerHtml = htmlHelper.ActionLink(text, action, controller, null, null).ToHtmlString();
            return new HtmlString(li.ToString());
        }

        public static IHtmlString MobileMenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller
        )
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.InnerHtml = htmlHelper.ActionLink(text, action, controller, null, new { @class = "ui-btn-active" }).ToHtmlString();
            }
            else
            {
                li.InnerHtml = htmlHelper.ActionLink(text, action, controller, null, null).ToHtmlString();
            }
            
            return new HtmlString(li.ToString());
        }

    }
}