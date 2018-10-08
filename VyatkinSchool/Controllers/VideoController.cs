using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VyatkinSchool.Models;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    public sealed class VideoController : SchoolBaseController
    {
        // GET: Video
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Show", "Gallery");
        }

        //GET: /Video/Add
        [AllowAnonymous]
        public ActionResult Add()
        {
            var viewModel = new VideoFileItem();
            return View(viewModel);
        }

        // POST: /Gallery/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(VideoFileItem viewModel, HttpPostedFileBase uploadVideoFile)
        {
            var videoFileItem = new VideoFileItem();

            if (ModelState.IsValid)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    if (uploadVideoFile != null)
                    {
                        string fileName = Path.GetFileName(uploadVideoFile.FileName);
                        int fileSize = uploadVideoFile.ContentLength;

                        var filePath = "~/VideoFileUpload/" + fileName;
                        var serverPath = Server.MapPath(filePath);
                        var directoryWithVideo = Path.GetDirectoryName(serverPath);
                        if (!new DirectoryInfo(directoryWithVideo).Exists)
                        {
                            Directory.CreateDirectory(directoryWithVideo);
                        }

                        uploadVideoFile.SaveAs(serverPath);

                        videoFileItem.Name = fileName;
                        videoFileItem.FilePath = filePath;
                        videoFileItem.FileSize = fileSize;
                        videoFileItem.Title = viewModel.Title;
                        videoFileItem.Description = viewModel.Description;
                        videoFileItem.IsDeleted = false;
                        dataBase.Video.Add(videoFileItem);
                        dataBase.SaveChanges();
                    }
                }
            }

            //Successfully created 
            return View("Created", videoFileItem);
        }

        //GET: /Video/Created
        public ActionResult Created(VideoFileItem viewModel)
        {
            using (var dataBase = new ApplicationDbContext())
            {
                return View(viewModel);
            }
        }

        //GET: /Video/Delete
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var videoFileItemForDelete = dataBase.Video.SingleOrDefault(videoFileItem => videoFileItem.Id == id);

                    if (videoFileItemForDelete != null)
                    {
                        return View(videoFileItemForDelete);
                    }
                }
            }

            return View();
        }

        // POST: /Gallery/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(VideoFileItem model)
        {
            model.IsDeleted = true;

            using (var dataBase = new ApplicationDbContext())
            {
                var videoFileItemForDelete = dataBase.Video.SingleOrDefault(VideoFileItem => VideoFileItem.Id == model.Id);
                if (videoFileItemForDelete != null)
                {
                    videoFileItemForDelete.IsDeleted = true;
                    dataBase.SaveChanges();

                    var serverPath = Server.MapPath(videoFileItemForDelete.FilePath);
                    if (System.IO.File.Exists(serverPath))
                    {
                        System.IO.File.Delete(serverPath);
                    }
                    
                    model = videoFileItemForDelete;
                }
            }

            //Successfully deleted 
            return View("Deleted", model);
        }

        //GET: /Video/Deleted
        public ActionResult Deleted()
        {
            return View();
        }

        //GET: /Video/Show
        [AllowAnonymous]
        public async Task<ActionResult> Show(int? page)
        {
            await IncrementPageCounters();

            using (var dataBase = new ApplicationDbContext())
            {
                var pageNumber = page ?? 1;

                var notDeletedVideo = dataBase.Video.Where(video => !video.IsDeleted)
                    .OrderByDescending(videoFileItem => videoFileItem.Id)
                    .ToList();

                var onePageOfVideo = notDeletedVideo.ToPagedList(pageNumber, 1);
                var currentGalleryGroupItem = onePageOfVideo.SingleOrDefault();

                return View(onePageOfVideo);
            }
        }

        //GET: /Video/ShowItem
        [AllowAnonymous]
        public async Task<ActionResult> ShowItem(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Show", "Video");
            }

            await IncrementPageCounters();

            using (var dataBase = new ApplicationDbContext())
            {
                var item = dataBase.Video.SingleOrDefault(videoFileItem => videoFileItem.Id == id);
                if (item != null)
                {
                    return View(item);
                }
            }
            return RedirectToAction("Show", "Video");
        }

        //GET: /Video/List
        public async Task<ActionResult> List(int? page)
        {
            await IncrementPageCounters();

            using (var dataBase = new ApplicationDbContext())
            {
                var pageNumber = page ?? 1;
                var notDeletedVideo = dataBase.Video.Where(video => !video.IsDeleted)
                    .OrderByDescending(videoFileItem => videoFileItem.Id)
                    .ToList();

                var onePageOfVideos = notDeletedVideo.ToPagedList(pageNumber, 10);
                return View(onePageOfVideos);
            }
        }

        //GET: /Video/Edit
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var videoItemForEdit = dataBase.Video.SingleOrDefault(videoFileItem => videoFileItem.Id == id);
                    if (videoItemForEdit != null)
                    {
                        return View(videoItemForEdit);
                    }
                }
            }

            return View();
        }

        // POST: /Message/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VideoFileItem viewModel, HttpPostedFileBase uploadVideoFile)
        {
            using (var dataBase = new ApplicationDbContext())
            {
                var itemForEdit = dataBase.Video.SingleOrDefault(item => item.Id == viewModel.Id);
                if (itemForEdit != null)
                {
                    if (uploadVideoFile != null)
                    {
                        string fileName = Path.GetFileName(uploadVideoFile.FileName);
                        int fileSize = uploadVideoFile.ContentLength;

                        var filePath = "~/VideoFileUpload/" + fileName;
                        uploadVideoFile.SaveAs(Server.MapPath(filePath));

                        viewModel.Name = fileName;
                        viewModel.FilePath = filePath;
                        viewModel.FileSize = fileSize;

                    }
                    else
                    {
                        viewModel.Name = itemForEdit.Name;
                        viewModel.FilePath = itemForEdit.FilePath;
                        viewModel.FileSize = itemForEdit.FileSize;
                    }

                    itemForEdit.IsDeleted = false;
                    itemForEdit.Title = viewModel.Title;
                    itemForEdit.Description = viewModel.Description;
                    itemForEdit.Name =  viewModel.Name;
                    itemForEdit.FilePath = viewModel.FilePath;
                    itemForEdit.FileSize = viewModel.FileSize;
                    dataBase.SaveChanges();
                }
            }

            //Successfully deleted 
            return View("Modified", viewModel);
        }

        [HttpGet]
        public FileStreamResult VideoFile(int? id)
        {
            if (id != null)
            {
                using (var dataBase = new ApplicationDbContext())
                {
                    var videoItem = dataBase.Video.SingleOrDefault(video => video.Id == id);
                    if (videoItem != null)
                    {
                        var videoCounterItem = dataBase.VideoDownloadsCounters.SingleOrDefault(videoCounter => videoCounter.Id == id);
                        if (videoCounterItem == null)
                        {
                            var videoDownloadCounterItem = new VideoDownloadCounterItem();
                            videoDownloadCounterItem.VideoFileId = (int)id;
                            videoDownloadCounterItem.DownloadCount = 1;
                            videoDownloadCounterItem.LastDownload = DateTime.Now;
                            dataBase.VideoDownloadsCounters.Add(videoDownloadCounterItem);
                        }
                        else
                        {
                            videoCounterItem.DownloadCount++;
                            videoCounterItem.LastDownload = DateTime.Now;
                        }
                        dataBase.SaveChanges();

                        var serverFilePath = Server.MapPath(videoItem.FilePath);
                        var fileStream = new FileStream(serverFilePath, FileMode.Open, FileAccess.Read);

                        var fileStreamResult = new FileStreamResult(fileStream, MimeMapping.GetMimeMapping(serverFilePath))
                        {
                            FileDownloadName = videoItem.Name
                        };
                        return fileStreamResult;
                    }
                }
            }

            return null;
        }
    }
}