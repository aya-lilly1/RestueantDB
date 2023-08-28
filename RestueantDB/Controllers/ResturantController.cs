using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RestueantDB.ModelViews;
using RestueantDB.Repostry.IServices;
using System;
using System.IO;

namespace RestueantDB.Controllers
{
    public class ResturantController : Controller
    {
        private IResturant _resturant;
        //private IWebHostEnvironment _host;
        public ResturantController(IResturant resturant)
        {
            this._resturant = resturant;
            //_host = host;
        }
        
        public IActionResult Index()
        {
            return View();
        }


        
        public IActionResult GetAllRestaurant()
        {
            var res = _resturant.GetAllResturant();
            return View(res);
        }

        [Authorize]
        public IActionResult AddResturant(bool IsSuccess =false , int idRes = 0)
        {
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.Id = idRes;

            return View();
        }
        [HttpPost]
     

        public IActionResult AddResturant(ResturantMV resturant,int rsID)
         {
         
            if (ModelState.IsValid)
            {
                int IdRest = _resturant.AddResturant(resturant );

                if (IdRest > 0)
                {
                     
                    //  ViewBag.Message = "Data Insert Successfully";
                    return RedirectToAction(nameof(AddResturant), new {  IsSuccess = true , idRes = IdRest});

                }
            }
            ViewBag.IsSuccess = false;

            return View();

        }

        public IActionResult AddNewResturant(ResturantMV resturant, int rsID)
        {

            if (ModelState.IsValid)
            {
                int IdRest = _resturant.AddResturant(resturant);

                if (IdRest > 0)
                {

                    //  ViewBag.Message = "Data Insert Successfully";
                    return RedirectToAction(nameof(AddResturant), new { IsSuccess = true, idRes = IdRest });

                }
            }
            ViewBag.IsSuccess = false;

            return View();

        }

        public IActionResult ShowDetails(int id)
        { 
            var res = _resturant.ShowDetails(id);
            return View(res);

        }
        public IActionResult GetAllRes()
        {
            var res = _resturant.GetAllResturant();
            return View(res);
        }




        public IActionResult Create()
        {
            ResturantMV resturant = new ResturantMV();

            return PartialView();
        }

    }
}
