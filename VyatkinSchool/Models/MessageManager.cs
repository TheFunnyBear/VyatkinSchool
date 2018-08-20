using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models
{
    using System;
    public class MessageManager
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, добавьте текст сообщения")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите заголовок для сообщения")]
        public string Title { get; set; }

        public Nullable<int> ImageID { get; set; }

        public DateTime Date { get; set; }
    }
}