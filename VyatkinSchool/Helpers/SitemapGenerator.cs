using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;
using VyatkinSchool.Models;

namespace VyatkinSchool.Helpers
{
    public sealed class SitemapGenerator
    {
        private readonly List<MessageItem> _notDeletedMessages;

        public SitemapGenerator(List<MessageItem> notDeletedMessages)
        {
            _notDeletedMessages = notDeletedMessages;
        }

        public IReadOnlyCollection<string> GetSitemapNodes(UrlHelper urlHelper)
        {
            var nodes = new List<string>();

            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "News", action = "SixNews" }));
            foreach (var message in _notDeletedMessages)
            {
                nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Message", action = "Show", id = message.Id }));
            }

            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Gallery", action = "Show" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Video", action = "Show" }));

            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "TimeTable" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "Contacts" }));

            return nodes;
        }

        public string GetSitemapDocument(IEnumerable<string> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (string sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }
    }
}