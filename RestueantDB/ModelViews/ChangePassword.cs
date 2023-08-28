using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestueantDB.ModelViews
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please Enter Your Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Please Enter Your New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please Enter Your  ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
