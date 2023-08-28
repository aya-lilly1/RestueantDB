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
    public class ResturantRepo : IResturant
    {
        private ApplicationDbContext _dbContext;
        private IMapper _mapper;
        private IWebHostEnvironment _host;





        public ResturantRepo(ApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment host)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _host = host;
        }



        public List<ResturantMV> GetAllResturant()
        {
            var result = _dbContext.Restueants.Where(x => x.IsDeleted == false).ToList();
            var res = _mapper.Map<List<ResturantMV>>(result);
            return res;
        }


        public int AddResturant(ResturantMV resturant)
        {
            if (resturant.ImgeFile != null)
            {
                string folder = "Uploads/";
                folder = UploadImage(folder, resturant.ImgeFile);
                resturant.ImageURL = folder;


                var result = new Resturant
                {
                    Name = resturant.Name,
                    Phone = resturant.Phone,
                    Address = resturant.Address,
                    Details = resturant.Details,
                    CreatedAt = System.DateTime.Now,
                    LastUpdate = System.DateTime.Now,
                    IsDeleted = false,
                    ImageURL = resturant.ImageURL

                };

                _dbContext.Restueants.Add(result);
                _dbContext.SaveChanges();
                return result.Id;

            }
            return 0;



        }



        public ResturantMV ShowDetails(int id)
        {
            var result = _dbContext.Restueants.Find(id);
            var res = _mapper.Map<ResturantMV>(result);
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
