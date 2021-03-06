﻿using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace HelloWorld
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }

    public class ProductRepository : IProductRepository
    {
        private static IEnumerable<Product> products;

        public IEnumerable<Product> Products
        {
            get
            {
                // Check if the MyProducts is NOT cached
                if (HttpContext.Current.Cache["MyProducts"] == null)
                {
                    var items = new[]
                    {
                        new Product{ Name = "C#",  Description="Learn C#", Price=200},
                        new Product{ Name = "ASP .NET MVC",  Description="Learn how to create websites", Price=250},
                        new Product{ Name = "Android",  Description="Learn how to write Android applications", Price=500},
                        new Product{ Name = "Design Patterns",  Description="Learn how to write Android applications", Price=300}
                    };

                    // Sliding Expiration
                    HttpContext.Current.Cache.Insert("MyProducts",
                                             items,
                                             null,
                                             Cache.NoAbsoluteExpiration,
                                             new TimeSpan(0,0,10));
                }

                return (IEnumerable<Product>)HttpContext.Current.Cache["MyProducts"];
            }
        }
    }
}