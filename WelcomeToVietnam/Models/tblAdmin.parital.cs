using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WelcomeToVietnam.Models
{
    [MetadataType(typeof(AdminMetadata))]
    public partial class tblAdmin
    {

    }
    public class AdminMetadata
    {
        [Required(ErrorMessage = "This feild is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}