using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestueantDB.Models;
using RestueantDB.ModelViews;
using RestueantDB.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestueantDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserServices _userServices;
        private IEmailSevicess _emailservices;

        public HomeController(ILogger<HomeController> logger, IUserServices userServices, IEmailSevicess emailservices)
        {
            _logger = logger;

            _userServices = userServices;
            _emailservices = emailservices;
        }
        public async Task<IActionResult>  Index()
        {
            //UserEmailOptions options = new UserEmailOptions
            //{
            //    ToEmail = new List<string> { "ayalilly1710@gmail.com" },

            //    Subject =  "Just  A test",
            //    PlaceHolders = new List<KeyValuePair<string, string>> {
            //        new KeyValuePair<string, string>("{{UserName}}","Aya Lilly")
            //        }             
                
             
            //};

         //   await _emailservices.SendTestEmail(options);


        //    var id = _userServices.GetUserId();
        //    var IsAuthenticated = _userServices.IsAuthenticated();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
