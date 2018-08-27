using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VyatkinSchool.Models
{
    public sealed class MessageItem
    {
        public int Id { get; set; }

        [Display(Name = "Двтв создания:")]
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Укажите файл с фотографией:")]
        public Nullable<int> ImageId { get; set; }

        [Display(Name = "Укажите заголовок сообщения:")]
        [Required(ErrorMessage = "Укажите заголовок сообщения.")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Укажите текст сообщения.")]
        [Display(Name = "Текст сообщения:")]
        public string MessageText { get; set; }
    }
}