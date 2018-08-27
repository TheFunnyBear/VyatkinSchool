using System;
using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models
{
    public sealed class GalleryItem
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public Nullable<int> GalleryId { get; set; }
        public bool IsVideo { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Укажите заголовок нового изображения:")]
        [Required(ErrorMessage = "Пожалуйста, введите заголовок для изображения")]
        public string Title { get; set; }

        [Display(Name = "Укажите описание для изображения:")]
        public string Description { get; set; }
    }
}