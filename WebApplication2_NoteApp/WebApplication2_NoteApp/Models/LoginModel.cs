using System.ComponentModel.DataAnnotations;

namespace WebApplication2_NoteApp.Models
{
    public class LoginModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez.")]
        public string Username { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş geçilemez.")]
        [MinLength(6), MaxLength(16)]
        public string Password { get; set; }
    }
}
