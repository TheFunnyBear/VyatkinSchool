using System;
using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models
{
    public sealed class VideoFileItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FileSize { get; set; }
        public string FilePath { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Укажите заголовок нового видео:")]
        [Required(ErrorMessage = "Пожалуйста, введите заголовок для изображения")]
        public string Title { get; set; }

        [Display(Name = "Укажите описание для видео:")]
        public string Description { get; set; }
    }
}