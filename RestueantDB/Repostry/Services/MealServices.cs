using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RestueantDB.Data;
using RestueantDB.Models;
using RestueantDB.ModelViews;
using RestueantDB.Repostry.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestueantDB.Repostry.Services
{
    public class MealServices : IMealServices
    {
        private ApplicationDbContext _dbContext;
        private IWebHostEnvironment _host;
        private IMapper _mapper;
        public MealServices(ApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment host)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _host = host;
        }
        public List<MealMV> GetMeal(int id)
        {
            var result = _dbContext.Meals.Where(x => x.ResturantId == id).ToList();
            var res = _mapper.Map<List<MealMV>>(result);
            return res;


        }
        public int AddMeal(MealMV meal,int resId)
        {
            if (meal.ImgeFile != null)
            {
                string folder = "Uploads/MealPhoto";
                folder = UploadImage(folder, meal.ImgeFile);
                meal.ImageURL = folder;
                var newMeal = new Meal
                {
                    ResturantId = resId,
                    Name = meal.Name,
                    Price = meal.Price,
                    Details = meal.Details,
                    ImageURL = meal.ImageURL,
                    CreatedAt = System.DateTime.Now,
                    LastUpdate = System.DateTime.Now,
                    IsDeleted = false
                };
                _dbContext.Meals.Add(newMeal);
                _dbContext.SaveChanges();
                return newMeal.Id;
            }
            return 0;

        }

        public MealMV ShowMealDetails(int id)
        {
            var result = _dbContext.Restueants.Find(id);
            var res = _mapper.Map<MealMV>(result);
            return res;

        }



        private string UploadImage(string folder, IFormFile ImgeFile)
        {
            folder += Guid.NewGuid().ToString() + "_" + ImgeFile.FileName;
            string ImageURL = "/" + folder;
            string serverFolder = Path.Combine(_host.WebRootPath, folder);
            ImgeFile.CopyTo(new FileStream(serverFolder, FileMode.Create));
            return ImageURL;
        }
    }
}
