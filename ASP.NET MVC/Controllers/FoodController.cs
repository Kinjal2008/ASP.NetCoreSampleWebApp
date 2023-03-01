using DataLibrary.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodData _foodData;

        public FoodController(IFoodData foodData)
        {
            _foodData = foodData;
        }  
        public async Task<IActionResult> Index()
        {
            var food = await _foodData.GetFood();
            return View(food);
        }
    }
}
