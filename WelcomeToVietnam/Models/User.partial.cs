using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WelcomeToVietnam.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Please confirm message")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
    public class UserMetadata
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This feild is requied")]
        [Remote("IsUserAvaiable", "Home", ErrorMessage = "This user is existed")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This feild is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This feild is required")]
        public string Gender { get; set; }

        [Range(1, 100, ErrorMessage = "Your age is not accuracy")]
        public Nullable<int> Age { get; set; }

        [Required(ErrorMessage = "this feild is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}