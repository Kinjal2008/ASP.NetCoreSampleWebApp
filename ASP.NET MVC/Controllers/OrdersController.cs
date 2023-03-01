using ASP.NET_MVC.Models;
using DataLibrary.Interface;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NET_MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public OrdersController(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var food = await _foodData.GetFood();

            OrderCreateModel model = new OrderCreateModel();
            food.ForEach(x =>
            {
                model.FoodItems.Add(new SelectListItem
                {
                    Value = x.Id.ToString(), Text = x.Title
                });
            });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderModel order)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            var food = await _foodData.GetFood();
            order.Total = order.Quantity * food.Single(x => x.Id == order.FoodId).Price;
            int id = await _orderData.CreateOrder(order);
            return RedirectToAction("Display", new {id});
        }

        public async Task<IActionResult> Display(int id)
        {
            OrderDisplayModel model = new OrderDisplayModel();
            model.Order = await _orderData.GetOrderById(id);
            if (model.Order != null)
            {
                var food = await _foodData.GetFood();
                model.ItemPurchased = food.FirstOrDefault(f => f.Id == model.Order.FoodId)?.Title;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string orderName)
        {
            await _orderData.UpdateOrder(id, orderName);
            return RedirectToAction("Display", new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderData.GetOrderById(id);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OrderModel order)
        {
            await _orderData.DeleteOrder(order.Id);
            return RedirectToAction("Create");
        }
    }
}
