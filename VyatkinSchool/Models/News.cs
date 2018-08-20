using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VyatkinSchool.Models
{
    public class News
    {
        public int NewsAtPage => 6;
        public int PageNumber { get; set; }
        public List<Message> SchoolNews { get; set; }
        public IPagedList<Message> OnePageOfMessages { get { return SchoolNews.ToPagedList(PageNumber, NewsAtPage); } }

        public News()
        {
            PageNumber = 0;

            SchoolNews = new List<Message>();
            InitData();
        }

        public int Pages { get { return SchoolNews.Count / NewsAtPage; } }


        public bool IsTopLeftExist()
        {
            return OnePageOfMessages.Any();
        }

        public bool IsTopCenterExist()
        {
            return IsItemWithIndexExist(index: 1);
        }

        public bool IsTopRightExist()
        {
            return IsItemWithIndexExist(index: 2);
        }

        public bool IsBottomLeftExist()
        {
            return IsItemWithIndexExist(index: 3);
        }

        public bool IsBottomCenterExist()
        {
            return IsItemWithIndexExist(index: 4);
        }

        public bool IsBottomRightExist()
        {
            return IsItemWithIndexExist(index: 5);
        }

        public Message TopLeft()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 0);
        }

        public Message TopCenter()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 1);
        }

        public Message TopRight()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 2);
        }

        public Message BottomLeft()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 3);
        }

        public Message BottomCenter()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 4);
        }

        public Message BottomRight()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 5);
        }

        private void InitData()
        {
            using (var dataBase = new GalleryManagerEntities())
            {
                SchoolNews.AddRange(dataBase.Messages.OrderByDescending(message => message.Date));
            }
        }

        private bool IsItemWithIndexExist(int index)
        {
            return OnePageOfMessages.Count > index;
        }

        private Message GetMeesageOnCurrentPageWithIndex(int index)
        {
            var messages = OnePageOfMessages;
            if (IsItemWithIndexExist(index: index))
            {
                return messages[index];
            }

            throw new Exception($"Can't get element with index [{index}]");
        }
    }
}