using System.Linq;

namespace OnlineShop.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
