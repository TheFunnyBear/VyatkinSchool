using System;
using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models
{
    public class GalleryData
    {
        public int ID { get; set; }

        public byte[] Image { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите заголовок для изображения")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите описание для изображения")]
        public string Description { get; set; }

        public Nullable<int> GalleryGroupID { get; set; }

        public Nullable<bool> IsVideo { get; set; }

        public Nullable<bool> IsVisible { get; set; }
    }
}