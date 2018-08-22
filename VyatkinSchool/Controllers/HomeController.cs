using System;
using System.Web.Mvc;
using VyatkinSchool.Models;
using System.Web;
using System.Linq;
using PagedList;
using VyatkinSchool.Infrastructure;

namespace VyatkinSchool.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int? page)
        {
            ViewBag.isAdminLogedOn = IsAdminLogedOn();
            var news = new News();
            var allMessages= news.SchoolNews;
            news.PageNumber = page ?? 1;
            return View(news);
        }

        public ActionResult Contacts()
        {
            var contacts = new Contacts();
            return View(contacts);
        }

        public ActionResult Gallery(int? page)
        {
            ViewBag.isAdminLogedOn = IsAdminLogedOn();
            var galleryPageModel = new GalleryPage();
            var picturesGroups = galleryPageModel.GroupsWithPicturesOnly;
            var pageNumber = page ?? 1; 
            var onePageOfPictures = picturesGroups.ToPagedList(pageNumber, 1); 
            ViewBag.OnePageOfPictures = onePageOfPictures;
            return View(galleryPageModel);
        }

        public ActionResult AddImage()
        {
            if(!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            var galleryModel = new GalleryManager();
            return View(galleryModel);
        }

        [HttpPost]
        public ActionResult AddImage(GalleryManager model, HttpPostedFileBase uploadImageFile)
        {
            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            GalleryGroup selectedGalleryGroup = null;

            if (ModelState.IsValid)
            {
                using (var dataBase = new VyatkinSchoolDbContext())
                {
                    if (uploadImageFile != null)
                    {
                        model.GalleryData.Image = new byte[uploadImageFile.ContentLength];
                        uploadImageFile.InputStream.Read(model.GalleryData.Image, 0, uploadImageFile.ContentLength);
                    }

                    selectedGalleryGroup = dataBase.GalleryGroup.Single(group => group.ID == model.SelectetGalleryGroup);
                    model.GalleryData.GalleryGroupID = selectedGalleryGroup.ID;
                    model.GalleryData.IsVideo = false;
                    model.GalleryData.IsVisible = true;
                    dataBase.Gallery.Add(model.GalleryData);
                    dataBase.SaveChanges();
                }
                ModelState.Clear();
            }

            var imageCreatedModel = new ImageCreated();
            imageCreatedModel.ImageId = model.GalleryData.ID;
            imageCreatedModel.Group = selectedGalleryGroup;
            return View("ImageCreated", imageCreatedModel);
        }

        public ActionResult AddImageGroup()
        {
            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            var galleryGroupModel = new GalleryGroup();
            return View(galleryGroupModel);
        }

        [HttpPost]
        public ActionResult AddImageGroup(GalleryGroup model)
        {
            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            var existedGroups = new ExistedGalleryGroups();
            existedGroups.CreatedGrop = model;

            if (ModelState.IsValid)
            {
                using (var dataBase = new VyatkinSchoolDbContext())
                {
                    if (dataBase.GalleryGroup.Any(group => group.GroupName.Equals(model.GroupName, StringComparison.OrdinalIgnoreCase)))
                    {
                        //Already exist
                        existedGroups.Succsessful = false;
                        existedGroups.AlreadyCreated = true;
                        existedGroups.Groups.AddRange(dataBase.GalleryGroup.ToList());
                        return View("ImageGroupCreated", existedGroups);
                    }
                    dataBase.GalleryGroup.Add(model);
                    dataBase.SaveChanges();
                    existedGroups.Groups.AddRange(dataBase.GalleryGroup.ToList());
                }
                ModelState.Clear();
            }

            //Successfully created 
            return View("ImageGroupCreated", existedGroups);
        }

        public ActionResult AddMessage()
        {
            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            var addMessageModel = new Message();
            return View(addMessageModel);
        }

        [HttpPost]
        public ActionResult AddMessage(Message model, HttpPostedFileBase uploadImageFile)
        {
            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            var galleryData = new GalleryData();
            var messageData = new Message();
            if (ModelState.IsValid)
            {
                using (var dataBase = new VyatkinSchoolDbContext())
                {
                    messageData.Date = DateTime.Now;
                    messageData.Title = model.Title;
                    messageData.MessageText = model.MessageText;

                    if (uploadImageFile != null)
                    {
                        galleryData.Image = new byte[uploadImageFile.ContentLength];
                        uploadImageFile.InputStream.Read(galleryData.Image, 0, uploadImageFile.ContentLength);
                        galleryData.IsVideo = false;
                        galleryData.IsVisible = true;
                        galleryData.Title = model.Title;
                        galleryData.Description = model.Title;
                        dataBase.Gallery.Add(galleryData);
                        dataBase.SaveChanges();
                        messageData.ImageId = galleryData.ID;
                    }
                    else
                    {
                        messageData.ImageId = null;
                    }

                    dataBase.Messages.Add(messageData);
                    dataBase.SaveChanges();
                }
                ModelState.Clear();
            }

            //Successfully created 
            return View("MessageCreated", messageData);
        }

        public ActionResult MessageCreated()
        {
            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            return View();
        }

        public ActionResult ImageGroupCreated()
        {
            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            return View();
        }

        public ActionResult ImageCreated()
        {
            if (!IsAdminLogedOn())
            {
                return GetDefaultPage();
            }

            return View();
        }

        public ActionResult TimeTable()
        {
            return View();
        }

        public ActionResult ShowMessage(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new VyatkinSchoolDbContext())
                {
                    var requerdMessage = dataBase.Messages.SingleOrDefault(message => message.Id == id);
                    if (requerdMessage != null)
                    {
                        return View(requerdMessage);
                    }
                }
            }

            return GetDefaultPage();
        }

        public ActionResult Login()
        {
            var loginPageModel = new LoginPage();
            return View(loginPageModel);
        }

        [HttpPost]
        public ActionResult Login(LoginPage model)
        {
            if (model.Password.Equals("123"))
            {
                System.Web.HttpContext.Current.Session["isAdminLogedOn"] = "true";
                return GetDefaultPage();
            }

            ModelState.Clear();
            return GetDefaultPage();
        }

        private bool IsAdminLogedOn()
        {
            var isAdminLogedOn = System.Web.HttpContext.Current.Session["isAdminLogedOn"] as string;
            return string.IsNullOrWhiteSpace(isAdminLogedOn) ? false : bool.Parse(isAdminLogedOn);
        }

        private ActionResult GetDefaultPage()
        {
            ViewBag.isAdminLogedOn = IsAdminLogedOn();
            var news = new News();
            var allMessages = news.SchoolNews;
            news.PageNumber = 1;
            return View("Index", news);
        }
    }
}