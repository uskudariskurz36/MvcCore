using System.ComponentModel.DataAnnotations;

namespace WebApplication2_NoteApp.Models
{
    public class ProfileModel
    {
        [Display(Name = "Ad")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(50)]
        public string Surname { get; set; }
    }
}
