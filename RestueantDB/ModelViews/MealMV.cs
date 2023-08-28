using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestueantDB.ModelViews
{
    public class MealMV
    {
        public int Id { get; set; }
        public int ResturantId { get; set; }
        [Required(ErrorMessage = "Please enter the name of meal")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the price of  meal")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please Enter the details of meal")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Please Enter Image")]
        [Display(Name = "Choose Photo")]
        public IFormFile ImgeFile { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
