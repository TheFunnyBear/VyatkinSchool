using PagedList;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VyatkinSchool.Models;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        // GET: Message
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("List", "Message");
        }

        // GET: /Message/Show
        [AllowAnonymous]
        public ActionResult Show(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var messageForShow = dataBase.Messages.SingleOrDefault(message => message.Id == id);
                    if(messageForShow != null)
                    {
                        return View(messageForShow);
                    }
                }
            }

            return View();
        }

        // GET: /Message/Add
        public ActionResult Add()
        {
            var message = new MessageItem();
            return View(message);
        }

        // POST: /Message/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MessageItem model, HttpPostedFileBase uploadImageFile)
        {
            var imageItem = new ImageItem();
            var galleryItem = new GalleryItem();
            var messageItem = new MessageItem();
            if (ModelState.IsValid)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    messageItem.Date = DateTime.Now;
                    messageItem.Title = model.Title;
                    messageItem.MessageText = model.MessageText;

                    if (uploadImageFile != null)
                    {
                        imageItem.Image = new byte[uploadImageFile.ContentLength];
                        uploadImageFile.InputStream.Read(imageItem.Image, 0, uploadImageFile.ContentLength);
                        dataBase.Images.Add(imageItem);
                        dataBase.SaveChanges();

                        galleryItem.ImageId = imageItem.Id;
                        galleryItem.IsVideo = false;
                        galleryItem.IsVisible = true;
                        galleryItem.Title = model.Title;
                        galleryItem.Description = string.Empty;
                        dataBase.Gallery.Add(galleryItem);
                        dataBase.SaveChanges();
                        messageItem.ImageId = galleryItem.Id;
                    }
                    else
                    {
                        messageItem.ImageId = null;
                    }

                    dataBase.Messages.Add(messageItem);
                    dataBase.SaveChanges();
                }
                ModelState.Clear();
            }

            //Successfully created 
            return View("Created", messageItem);
        }

        // GET: /Message/Edit
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var messageForEdit = dataBase.Messages.SingleOrDefault(message => message.Id == id);
                    if (messageForEdit != null)
                    {
                        return View(messageForEdit);
                    }
                }
            }

            return View();
        }

        // POST: /Message/Edit
        [HttpPost]
        public ActionResult Edit(MessageItem model, HttpPostedFileBase uploadImageFile)
        {
            var imageItem = new ImageItem();
            var galleryItem = new GalleryItem();
            using (var dataBase = new ApplicationDbContext())
            {
                var itemForEdit = dataBase.Messages.SingleOrDefault(message => message.Id == model.Id);
                if (itemForEdit != null)
                {
                    if (uploadImageFile != null)
                    {
                        imageItem.Image = new byte[uploadImageFile.ContentLength];
                        uploadImageFile.InputStream.Read(imageItem.Image, 0, uploadImageFile.ContentLength);
                        dataBase.Images.Add(imageItem);
                        dataBase.SaveChanges();

                        galleryItem.ImageId = imageItem.Id;
                        galleryItem.IsVideo = false;
                        galleryItem.IsVisible = true;
                        galleryItem.Title = model.Title;
                        galleryItem.Description = string.Empty;
                        dataBase.Gallery.Add(galleryItem);
                        dataBase.SaveChanges();
                        itemForEdit.ImageId = galleryItem.Id;
                        model.ImageId = galleryItem.Id;
                    }
                    else
                    {
                        model.ImageId = itemForEdit.ImageId;
                    }
                    model.Date = itemForEdit.Date;

                    itemForEdit.Title = model.Title;
                    itemForEdit.MessageText = model.MessageText;
                    itemForEdit.IsDeleted = false;
                    dataBase.SaveChanges();
                }

            }

            //Successfully deleted 
            return View("Modified", model);
        }

        // GET: /Message/Delete
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var messageForDelete = dataBase.Messages.SingleOrDefault(message => message.Id == id);
                    if (messageForDelete != null)
                    {
                        return View(messageForDelete);
                    }
                }
            }

            return View();
        }

        // POST: /Message/Delete
        [HttpPost]
        public ActionResult Delete(MessageItem model)
        {
            model.IsDeleted = true;

            using (var dataBase = new ApplicationDbContext())
            {
                var itemForDelete = dataBase.Messages.SingleOrDefault(message => message.Id == model.Id);
                if (itemForDelete != null)
                {
                    itemForDelete.IsDeleted = true;
                    dataBase.SaveChanges();
                    model = itemForDelete;
                }
            }

            //Successfully deleted 
            return View("Deleted", model);
        }

        // GET: /Message/Created
        public ActionResult Created()
        {
            return View();
        }

        // GET: /Message/Deleted
        public ActionResult Deleted()
        {
            return View();
        }

        // GET: /Message/Modified
        public ActionResult Modified()
        {
            return View();
        }

        // GET: /Message/List
        public ActionResult List(int? page)
        {
            using (var dataBase = new ApplicationDbContext())
            {
                var pageNumber = page ?? 1;
                var notDeletedMessages = dataBase.Messages
                    .Where(message => !message.IsDeleted)
                    .OrderByDescending(message => message.Date)
                    .ToList();
                var onePageOfMessages = notDeletedMessages.ToPagedList(pageNumber, 10);
                ViewBag.OnePageOfMessages = onePageOfMessages;
                return View();
            }
        }
    }
}