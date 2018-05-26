using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using System.Linq;

namespace OnlineShop.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository productRepository;

        public AdminController(IProductRepository repo)
        {
            productRepository = repo;
        }

        public ViewResult Index() => View(productRepository.Products);

        public ViewResult Edit(int productID) =>
            View(productRepository.Products.FirstOrDefault(o => o.ProductID == productID));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is smth wrong with data
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productID)
        {
            Product deletedProduct = productRepository.DeleteProduct(productID);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was delted";
            }
            return RedirectToAction("Index");
        }
    }
}