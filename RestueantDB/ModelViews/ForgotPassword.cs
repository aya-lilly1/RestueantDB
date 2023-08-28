using System.ComponentModel.DataAnnotations;

namespace RestueantDB.ModelViews
{
    public class ForgotPassword
    {
        [Required,EmailAddress,Display(Name ="Enter Your Email")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
