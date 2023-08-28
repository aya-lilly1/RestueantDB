using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestueantDB.ModelViews
{
    public class SignUpUser
    {
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [Remote(action:"ValiateEmail" , controller:"Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }



    }
}
