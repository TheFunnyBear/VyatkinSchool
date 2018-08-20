using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VyatkinSchool.Models
{
    public sealed class Message
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

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