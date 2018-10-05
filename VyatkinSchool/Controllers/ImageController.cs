using Nito.AsyncEx;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VyatkinSchool.Models;
using VyatkinSchool.Models.IdentityModels;

namespace VyatkinSchool.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ImageController : SchoolBaseController
    {
        private readonly AsyncLock _mutex = new AsyncLock();

        [AllowAnonymous]
        public async Task<ActionResult> Show(int id)
        {
            await IncrementPageCounters();
            using (var dataBase = new ApplicationDbContext())
            {
                var galleryItem = dataBase.Images.SingleOrDefault(item => item.Id == id);
                var fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";

                var fileContent = galleryItem.Image;
                if (IsJpegOrPngImage(fileContent))
                {
                    fileContent = WatermarkImage(fileContent);
                }

                return File(fileContent, "image/jpg", fileName);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ShowImageForGalleryItem(int id)
        {
            await IncrementPageCounters();
            using (var dataBase = new ApplicationDbContext())
            {
                var gallery = dataBase.Gallery.SingleOrDefault(galleryItem => galleryItem.Id == id);
                var imageItem = dataBase.Images.SingleOrDefault(item => item.Id == gallery.ImageId);
                var fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";

                var fileContent = imageItem.Image;
                if (IsJpegOrPngImage(fileContent))
                {
                    fileContent = WatermarkImage(fileContent);
                }

                return File(fileContent, "image/jpg", fileName);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ShowPreview(int id)
        {
            await IncrementPageCounters();
            using (var dataBase = new ApplicationDbContext())
            {
                byte[] fileContent = null;
                var currentPreviewItem = dataBase.PreviewItems.SingleOrDefault(previewItem => previewItem.ImageId == id);
                if (currentPreviewItem != null)
                {
                    fileContent = currentPreviewItem.Image;
                }
                else
                {
                    var galleryItem = dataBase.Images.SingleOrDefault(item => item.Id == id);

                    fileContent = galleryItem.Image;
                    if(IsJpegOrPngImage(fileContent))
                    {
                        var resizedImage = ResizeImage(fileContent, width: 300, height: 225);
                        var resizedImageBytes = (byte[])new ImageConverter().ConvertTo(resizedImage, typeof(byte[]));

                        var previewItem = new PreviewItem();
                        previewItem.ImageId = id;
                        previewItem.Image = new byte[resizedImageBytes.Length];
                        Array.Copy(resizedImageBytes, previewItem.Image, resizedImageBytes.Length);
                        dataBase.PreviewItems.Add(previewItem);
                        dataBase.SaveChanges();

                        fileContent = previewItem.Image;
                    }
                }

                if(IsJpegOrPngImage(fileContent))
                {
                    fileContent = WatermarkImage(fileContent);
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";
                return File(fileContent, "image/jpg", fileName);
            }
        }

        public byte[] WatermarkImage(byte[] jpegByteArray)
        {
            const string watermarkFilePath = "~/Content/Watermark/Watermark.png";
            var fileName = Server.MapPath(watermarkFilePath);
            Bitmap watermark = new Bitmap(fileName);

            Image image = (Bitmap)new ImageConverter().ConvertFrom(jpegByteArray);
            using (Graphics imageGraphics = Graphics.FromImage(image))
            {
                watermark.SetResolution(imageGraphics.DpiX, imageGraphics.DpiY);
                watermark.MakeTransparent();

                int x = image.Width - watermark.Width;
                int y = image.Height - watermark.Height;

                imageGraphics.DrawImage(watermark, x, y, watermark.Width, watermark.Height);
            }

            return (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
        }

        public bool IsJpegOrPngImage(byte[] jpegByteArray)
        {
            try
            {
                Image image = (Bitmap)new ImageConverter().ConvertFrom(jpegByteArray);
                return image.RawFormat.Equals(ImageFormat.Jpeg)|| image.RawFormat.Equals(ImageFormat.Png);
            }
            catch (OutOfMemoryException)
            {
                return false;
            }
        }

        public static Bitmap ResizeImage(byte[] jpegByteArray, int width, int height)
        {
            Image image = (Bitmap)new ImageConverter().ConvertFrom(jpegByteArray);
            return ResizeImage(image, width, height);
        }
        
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}