using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OnlineShop.Models;

namespace OnlineShop.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository productRepository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            productRepository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View(productRepository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
