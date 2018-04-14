using Moq;
using OnlineShop.Controllers;
using OnlineShop.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OnlineShop.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products)
                .Returns((
                new Product[] 
                {
                    new Product
                    {
                        ProductID = 1,
                        Name = "P1"
                    },
                    new Product
                    {
                        ProductID = 2,
                        Name = "P2"
                    },
                    new Product
                    {
                        ProductID = 3,
                        Name = "P3"
                    },
                    new Product
                    {
                        ProductID = 4,
                        Name = "P4"
                    },
                    new Product
                    {
                        ProductID = 5,
                        Name = "P5"
                    }
                }).AsQueryable<Product>());

            ProductController productController = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            // ACT
            IEnumerable<Product> result = productController.List(2).ViewData.Model as IEnumerable<Product>;

            // Assert 
            Product[] productsArray = result.ToArray();
            Assert.True(productsArray.Length == 2);
            Assert.Equal("P4", productsArray[0].Name);
            Assert.Equal("P5", productsArray[1].Name);
        }
    }
}
