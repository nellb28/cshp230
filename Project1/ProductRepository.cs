using HelloWorld.Models;
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
                        new Product{ Name = "ACCY 201",  Description="Introduction to accounting", Price=500},
                        new Product{ Name = "CS 230",  Description="ASP .NET Programming", Price=1000},
                        new Product{ Name = "CALC 204",  Description="Differential Equations", Price=1000}
                    };

                    // Absolute Expiration
                    //HttpContext.Current.Cache.Insert("MyProducts",
                    //                         items,
                    //                         null,
                    //                         DateTime.Now.AddSeconds(10),
                    //                         Cache.NoSlidingExpiration);

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