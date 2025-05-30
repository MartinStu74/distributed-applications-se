using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fitness_Shop.ActionFilters;
using Fitness_Shop.ExtentionMethods;
using Fitness_Shop.Models;
using Fitness_Shop.Repositories;
using Fitness_Shop.ViewModels.Cart;
using System.Collections.Generic;

namespace Fitness_Shop.Controllers
{
    [AuthenticationFilter]
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart";

        // Add an item to the cart
        public IActionResult AddToCart(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            List<Product> cart = this.HttpContext.Session.GetObject<List<Product>>(CartSessionKey) ?? new List<Product>(); //list ot producti(kolichka) v sesiqta 

            Product product = productRepository.GetFirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                if (cart.FirstOrDefault(p => p.Id == product.Id) != null)
                {
                    return RedirectToAction("Index", "Home");
                    // za da ne dobavqsh dreha vtori put
                }
                else
                {
                    cart.Add(product);
                    HttpContext.Session.SetObject(CartSessionKey, cart);


                }

            }
            return RedirectToAction("Index", "Cart");


        }

        // View the cart
        public IActionResult Index()
        {
            IndexVM model = new IndexVM();
            model.Products = this.HttpContext.Session.GetObject<List<Product>>(CartSessionKey) ?? new List<Product>();
            return View(model);
        }

        // Remove an item from the cart
        public IActionResult RemoveFromCart(int id)
        {
            List<Product> cart = HttpContext.Session.GetObject<List<Product>>(CartSessionKey) ?? new List<Product>();

            Product item = cart.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                cart.Remove(item);
            }

            HttpContext.Session.SetObject(CartSessionKey, cart);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Buy()
        {
            OrderRepository orderrepo = new OrderRepository();
            OrderProductRepository orderproductrepo = new OrderProductRepository();
            List<Product> cart = HttpContext.Session.GetObject<List<Product>>(CartSessionKey) ?? new List<Product>();
            User user = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (cart.Count > 0 && user != null)
            {
                Order order = new Order();
                order.UserId = user.Id;
                order.OrderDate = DateTime.Now;
                order.TotalPrice = cart.Sum(x => x.Price);

                orderrepo.Save(order);


                foreach (Product product in cart)
                {

                    OrderProduct orderProduct = new OrderProduct();
                    orderProduct.ProductId = product.Id;
                    orderProduct.OrderId = order.Id;

                    //order.OrderProducts.Add(orderProduct);
                    orderproductrepo.Save(orderProduct);
                }


            }


            this.HttpContext.Session.SetObject<List<Product>>(CartSessionKey, null);
            return RedirectToAction("Index", "Cart");
        }
    }
}
