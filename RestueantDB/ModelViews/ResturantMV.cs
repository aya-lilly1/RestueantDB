using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestueantDB.ModelViews
{
    public class ResturantMV
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Resturant Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Resturant Phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Resturant Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Resturant Details")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Please Enter Image")]
        [Display(Name ="Choose Photo")]
        public IFormFile ImgeFile { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
