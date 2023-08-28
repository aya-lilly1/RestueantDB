using System.ComponentModel.DataAnnotations;

namespace RestueantDB.ModelViews
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Please Enter Your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display( Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
