using System.ComponentModel.DataAnnotations;

namespace WebApplication2_NoteApp.Models
{
    public class ProfilePictureModel
    {
        [Required]
        public IFormFile NewPicture { get; set; }
    }
}
