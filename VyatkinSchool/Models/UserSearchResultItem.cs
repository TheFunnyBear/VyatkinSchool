using System;

namespace VyatkinSchool.Models
{
    public sealed class UserSearchResultItem
    {
        public ContentType ContntType { get; set; }
        public int ContentId { get; set; }
        public int RequestId { get; set; }
        public string SearchTitle { get; set; }
        public string SearchText { get; set; }

        public string GetController()
        {
            switch(ContntType)
            {
                case ContentType.News:
                    return "Message";
                case ContentType.Gallery:
                    return "Gallery";
                case ContentType.Video:
                    return "Video";
                default:
                    throw new ArgumentOutOfRangeException($"Not supported content type :[{ContntType}].");
            }

        }

        public string GetAction()
        {
            switch (ContntType)
            {
                case ContentType.News:
                    return "Show";
                case ContentType.Gallery:
                case ContentType.Video:
                    return "ShowItem";

                default:
                    throw new ArgumentOutOfRangeException($"Not supported content type :[{ContntType}].");
            }
        }
    }
}