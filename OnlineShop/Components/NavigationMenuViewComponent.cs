using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public string Invoke()
        {
            return "Hello from Nav View Component";
        }
    }
}
