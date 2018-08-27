using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Models
{
    public sealed class SixNewsViewModel
    {
        static public int NewsAtPage => 6;

        public int PageNumber { get; set; }
        public List<MessageItem> SchoolNews { get; set; }
        public IPagedList<MessageItem> OnePageOfMessages { get { return SchoolNews.ToPagedList(PageNumber, NewsAtPage); } }

        public SixNewsViewModel()
        {
            PageNumber = 0;
            SchoolNews = new List<MessageItem>();
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

        public MessageItem TopLeft()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 0);
        }

        public MessageItem TopCenter()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 1);
        }

        public MessageItem TopRight()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 2);
        }

        public MessageItem BottomLeft()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 3);
        }

        public MessageItem BottomCenter()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 4);
        }

        public MessageItem BottomRight()
        {
            return GetMeesageOnCurrentPageWithIndex(index: 5);
        }

        private void InitData()
        {
            using (var dataBase = new ApplicationDbContext())
            {
                var notDeletedMessages = dataBase.Messages
                    .Where(message => !message.IsDeleted)
                    .OrderByDescending(message => message.Date)
                    .ToList();

                SchoolNews.AddRange(notDeletedMessages);
            }
        }

        private bool IsItemWithIndexExist(int index)
        {
            return OnePageOfMessages.Count > index;
        }

        private MessageItem GetMeesageOnCurrentPageWithIndex(int index)
        {
            var messages = OnePageOfMessages;
            if (!IsItemWithIndexExist(index: index))
            {
                throw new ArgumentOutOfRangeException($"Can't get element with index [{index}]");
            }

            return messages[index];

        }
    }
}