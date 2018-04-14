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
    }
}
                                                        