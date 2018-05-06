using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Infrastructure;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnURL)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnURL = returnURL
            });
        }

        public RedirectToActionResult AddToCart(int productID, string returnURL)
        {
            Product product = repository.Products.FirstOrDefault(x => x.ProductID == productID);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnURL });
        }

        public RedirectToActionResult RemoveFromCart(int productID, string returnURL)
        {
            Product product = repository.Products.FirstOrDefault(x => x.ProductID == productID);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnURL });
        }
    }
}
