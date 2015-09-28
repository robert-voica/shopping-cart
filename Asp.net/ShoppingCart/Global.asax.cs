using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ShoppingCart.Models;

namespace ShoppingCart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FillTestData();
        }

        protected void FillTestData() { 
            var db = new CartModel();

            if (!db.Products.Any()) {
                db.Products.Add(new Product() {
                    Name = "Intel Core i7-4790K",
                    Price = 325
                });

                db.Products.Add(new Product() {
                    Name = "Intel Core i7-5775C",
                    Price = 403
                });

                db.Products.Add(new Product() {
                    Name = "Intel Core i7-3970X",
                    Price = 620
                });

                db.Products.Add(new Product() {
                    Name = "Intel Core i7-4960X",
                    Price = 1000
                });

                db.Products.Add(new Product() {
                    Name = "Intel Core i7-6700",
                    Price = 325
                });

                db.Products.Add(new Product() {
                    Name = "Intel Core i7-2600S",
                    Price = 280
                });

                db.Products.Add(new Product() {
                    Name = "Intel Core i7-4790K",
                    Price = 360
                });

                db.Products.Add(new Product() {
                    Name = "Intel Core i7-2600",
                    Price = 315
                });

                db.SaveChanges();
            }

            db.Dispose();
        }
    }
}
