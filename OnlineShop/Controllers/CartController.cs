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

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returnURL)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnURL = returnURL
            });
        }

        public RedirectToActionResult AddToCart(int productID, string returnURL)
        {
            Product product = repository.Products.FirstOrDefault(x => x.ProductID == productID);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnURL });
        }

        public RedirectToActionResult RemoveFromCart(int productID, string returnURL)
        {

            Product product = repository.Products.FirstOrDefault(x => x.ProductID == productID);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnURL });

        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
