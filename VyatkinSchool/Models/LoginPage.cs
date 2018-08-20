using System.ComponentModel.DataAnnotations;

namespace VyatkinSchool.Models
{
    public class LoginPage
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пароль для управления публикациями")]
        public string Password { get; set; }
    }
}