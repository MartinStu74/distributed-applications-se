using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Fitness_Shop.ExtentionMethods;
using Fitness_Shop.Models;
using Fitness_Shop.Repositories;
using Fitness_Shop.ViewModels.Home;

namespace Fitness_Shop.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        [HttpPost]
        public IActionResult Index(string? selectedCategory, string? searchQuery)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ProductRepository productRepository = new ProductRepository();
            IndexVM model = new IndexVM();

            model.Categories = categoryRepository.GetAll();
            model.SelectedCategory = selectedCategory;

            var products = productRepository.GetAll();

            if (!string.IsNullOrEmpty(selectedCategory))
            {
                products = products.Where(x => x.Category.Name == selectedCategory).ToList();
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(x => x.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            model.Products = products;

            return View(model);
        }


        [HttpGet]

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]

        public IActionResult SignUp(SignUpVM model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            UserRepository userRepository = new UserRepository();

            User newUser = new User();


            newUser.Username = model.Username;
            newUser.Password = model.Password;

            userRepository.Save(newUser);



            this.HttpContext.Session.SetObject<User>("loggedUser", newUser);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(LoginVM model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            UserRepository userRepo = new UserRepository();

            User loggedUser = userRepo.GetFirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (loggedUser == null)
            {
                return View(model);
            }
            else
            {
                this.HttpContext.Session.SetObject<User>("loggedUser", loggedUser);
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpGet]
        public IActionResult Logout()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (loggedUser != null)
            {
                this.HttpContext.Session.SetObject<User>("loggedUser", null);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
