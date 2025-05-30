using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fitness_Shop.ActionFilters;
using Fitness_Shop.ExtentionMethods;
using Fitness_Shop.Models;
using Fitness_Shop.Repositories;
using Fitness_Shop.ViewModels.Orders;

namespace Fitness_Shop.Controllers
{
    [AuthenticationFilter]
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            OrderRepository repo = new OrderRepository();

            IndexVM model = new IndexVM();
            User user = this.HttpContext.Session.GetObject<User>("loggedUser");


            model.Orders = repo.GetAll(x => x.UserId == user.Id);
            return View(model);
        }
    }
}
