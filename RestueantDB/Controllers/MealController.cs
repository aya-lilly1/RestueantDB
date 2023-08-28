using Microsoft.AspNetCore.Mvc;
using RestueantDB.ModelViews;
using RestueantDB.Repostry.IServices;

namespace RestueantDB.Controllers
{
    public class MealController : Controller
    {
        private IMealServices _mealServices;
        public MealController(IMealServices mealServices)
        {
            _mealServices = mealServices;

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMeal()
        {

            return View();
        }
        public IActionResult AddMeal(int id, bool IsSuccess = false, int idMeal = 0)
        {
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.Id = idMeal;
            ViewBag.resId = id;
            return View();
        }
       // [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddMeal(MealMV meal ,int resId)
        {
            if (ModelState.IsValid)
            {
                var id = _mealServices.AddMeal(meal, resId);
                if(id > 0)
                {
                    return RedirectToAction(nameof(AddMeal), new { IsSuccess = true,idMeal=id }) ;
                }
            }
            ViewBag.IsSuccess = false;
            return View();
        }
        public IActionResult ShowMealDetails(int id)
        {
            var res = _mealServices.ShowMealDetails(id);
            return View(res);

        }

    }
}
