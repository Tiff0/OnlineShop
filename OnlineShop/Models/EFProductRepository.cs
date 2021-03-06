﻿using OnlineShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.FirstOrDefault(o => o.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEbtry = context.Products.FirstOrDefault(x => x.ProductID == productID);
            if (dbEbtry != null)
            {
                context.Products.Remove(dbEbtry);
                context.SaveChanges();
            }

            return dbEbtry;
        }
    }
}
                                                        