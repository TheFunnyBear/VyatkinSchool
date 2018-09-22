using PagedList;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VyatkinSchool.Models;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    [RequireHttps]
    public class GalleryController : SchoolBaseController
    {
        // GET: Gallery
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Show", "Gallery");
        }

        // GET: /Gallery/Add
        [AllowAnonymous]
        public ActionResult Add()
        {
            var viewModel = new GalleryItem();
            using (var dataBase = new ApplicationDbContext())
            {
                var galleryGroups = dataBase.GalleryGroups.ToList();
                ViewBag.GalleryGroupsSelectList = new SelectList(galleryGroups, "Id", "GroupName");
                return View(viewModel);
            }
        }

        // POST: /Gallery/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(GalleryItem viewModel, HttpPostedFileBase uploadImageFile)
        {
            var imageItem = new ImageItem();
            var galleryItem = new GalleryItem();

            if (ModelState.IsValid)
            {
                using (var dataBase = new ApplicationDbContext())
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
                        galleryItem.Title = viewModel.Title;
                        galleryItem.Description = viewModel.Description;
                        galleryItem.GalleryId = viewModel.GalleryId;
                        dataBase.Gallery.Add(galleryItem);
                        dataBase.SaveChanges();

                        viewModel.ImageId = imageItem.Id;
                        var gallery = dataBase.GalleryGroups.SingleOrDefault(group => group.Id == viewModel.GalleryId);
                        if (gallery != null)
                        {
                            ViewBag.GroupName = gallery.GroupName;
                        }
                    }
                }
            }

            //Successfully created 
            return View("Created",  viewModel);
        }

        // GET: /Gallery/Created
        public ActionResult Created(GalleryItem viewModel)
        {
            using (var dataBase = new ApplicationDbContext())
            {
                var gallery = dataBase.GalleryGroups.SingleOrDefault(group => group.Id == viewModel.GalleryId);
                if (gallery != null)
                {
                    ViewBag.GroupName = gallery.GroupName;
                }

                return View(viewModel);
            }
        }

        // GET: /Gallery/Delete
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var galleryItemForDelete = dataBase.Gallery.SingleOrDefault(galleryItem => galleryItem.Id == id);
                    ViewBag.GalleryGroups = dataBase.GalleryGroups.ToList();
                    if (galleryItemForDelete != null)
                    {
                        return View(galleryItemForDelete);
                    }
                }
            }

            return View();
        }

        // POST: /Gallery/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(GalleryItem model)
        {
            model.IsDeleted = true;

            using (var dataBase = new ApplicationDbContext())
            {
                var galleryItemForDelete = dataBase.Gallery.SingleOrDefault(galleryItem => galleryItem.Id == model.Id);
                if (galleryItemForDelete != null)
                {
                    galleryItemForDelete.IsDeleted = true;
                    dataBase.SaveChanges();
                    model = galleryItemForDelete;
                }
            }

            //Successfully deleted 
            return View("Deleted", model);
        }

        // GET: /Gallery/Deleted
        public ActionResult Deleted()
        {
            return View();
        }

        // GET: /Gallery/Show
        [AllowAnonymous]
        public ActionResult Show(int? page)
        {
            IncrementPageCounters();
            using (var dataBase = new ApplicationDbContext())
            {
                var pageNumber = page ?? 1;

                var galleryGroupsWithImages = dataBase.GalleryGroups
                    .Where(galleryGroup => dataBase.Gallery.Any(galleryItem => 
                        galleryItem.GalleryId == galleryGroup.Id 
                        && !galleryItem.IsDeleted 
                        && galleryItem.IsVisible 
                        && !galleryItem.IsVideo))
                        .OrderByDescending(galleryGroup => galleryGroup.Id)
                        .ToList();

                var onePageOfGalleryGroups = galleryGroupsWithImages.ToPagedList(pageNumber, 1);
                var currentGalleryGroupItem = onePageOfGalleryGroups.Single(); 
                var galleryItems = dataBase.Gallery
                    .Where(galleryItem =>
                       galleryItem.GalleryId == currentGalleryGroupItem.Id
                       && !galleryItem.IsDeleted
                       && galleryItem.IsVisible
                       && !galleryItem.IsVideo)
                       .ToList();

                ViewBag.GalleryItems = galleryItems;
                ViewBag.ItemsCount = galleryItems.Count();
                return View(onePageOfGalleryGroups);
            }
        }

        // GET: /Gallery/ShowItem
        [AllowAnonymous]
        public ActionResult ShowItem(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Show", "Gallery");
            }

            IncrementPageCounters();
            using (var dataBase = new ApplicationDbContext())
            {
                var item = dataBase.Gallery.SingleOrDefault(galleryItem => galleryItem.Id == id);
                if (item != null)
                {
                    ViewBag.GalleryGroups = dataBase.GalleryGroups.ToList();
                    return View(item);
                }
            }
            return RedirectToAction("Show", "Gallery");
        }

        // GET: /Gallery/List
        public ActionResult List(int? page)
        {
            IncrementPageCounters();

            using (var dataBase = new ApplicationDbContext())
            {
                var pageNumber = page ?? 1;
                var notDeletedImages = dataBase.Gallery
                    .Where(galleryItem => !galleryItem.IsDeleted)
                    .Where(galleryItem => galleryItem.GalleryId != null)
                    .OrderByDescending(message => message.Id)
                    .ToList();

                ViewBag.GalleryGroups = dataBase.GalleryGroups.ToList();
                var onePageOfPictures = notDeletedImages.ToPagedList(pageNumber, 10);
                return View(onePageOfPictures);
            }
        }

        // GET: /Gallery/Edit
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var galliryItemForEdit = dataBase.Gallery.SingleOrDefault(galleryItem => galleryItem.Id == id);
                    if (galliryItemForEdit != null)
                    {
                        var galleryGroups = dataBase.GalleryGroups.ToList();
                        ViewBag.GalleryGroupsSelectList = new SelectList(galleryGroups, "Id", "GroupName");
                        return View(galliryItemForEdit);
                    }
                }
            }

            return View();
        }

        // POST: /Message/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GalleryItem  model, HttpPostedFileBase uploadImageFile)
        {
            var imageItem = new ImageItem();
            var galleryItem = new GalleryItem();
            
            galleryItem.IsVideo = false;
            galleryItem.IsVisible = true;
            galleryItem.Title = model.Title;
            galleryItem.Description = model.Description;
            galleryItem.GalleryId = model.Id;

            using (var dataBase = new ApplicationDbContext())
            {
                var itemForEdit = dataBase.Gallery.SingleOrDefault(item => item.Id == model.Id);
                if (itemForEdit != null)
                {
                    if (uploadImageFile != null)
                    {
                        imageItem.Image = new byte[uploadImageFile.ContentLength];
                        uploadImageFile.InputStream.Read(imageItem.Image, 0, uploadImageFile.ContentLength);
                        dataBase.Images.Add(imageItem);
                        dataBase.SaveChanges();
                        itemForEdit.ImageId = imageItem.Id;
                        model.ImageId = imageItem.Id;
                    }
                    else
                    {
                        model.ImageId = itemForEdit.ImageId;
                    }

                    itemForEdit.IsVideo = false;
                    itemForEdit.IsVisible = true;
                    itemForEdit.Title = model.Title;
                    itemForEdit.Description = model.Description;
                    itemForEdit.GalleryId = model.GalleryId;
                    itemForEdit.IsDeleted = false;
                    dataBase.SaveChanges();
                }

                var galleryGroups = dataBase.GalleryGroups.SingleOrDefault(group => group.Id == model.GalleryId);
                if (galleryGroups != null)
                {
                    ViewBag.GroupName = galleryGroups.GroupName;
                }
            }

            //Successfully deleted 
            return View("Modified", model);
        }
    }
}