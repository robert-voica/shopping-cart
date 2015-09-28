using System;
using Starcounter;

namespace ShoppingCart {
    class Program {
        static void Main() {
            FillTestData();

            Handle.GET("/shoppingcart", () => {
                return Db.Scope<Json>(() => {
                    IndexPage page = new IndexPage();

                    page.Session = new Session(SessionOptions.PatchVersioning);
                    page.Products.Data = Db.SQL<Product>("SELECT p FROM ShoppingCart.Product p ORDER BY p.Name");
                    page.CreateNewOrder();

                    return page;
                });
            });
        }

        static void FillTestData() {
            if (Db.SQL("SELECT p FROM ShoppingCart.Product p").First != null) {
                return;
            }

            Db.Transact(() => {
                new Product() {
                    Name = "Intel Core i7-4790K",
                    Price = 325
                };

                new Product() {
                    Name = "Intel Core i7-5775C",
                    Price = 403
                };

                new Product() {
                    Name = "Intel Core i7-3970X",
                    Price = 620
                };

                new Product() {
                    Name = "Intel Core i7-4960X",
                    Price = 1000
                };

                new Product() {
                    Name = "Intel Core i7-6700",
                    Price = 325
                };

                new Product() {
                    Name = "Intel Core i7-2600S",
                    Price = 280
                };

                new Product() {
                    Name = "Intel Core i7-4790K",
                    Price = 360
                };

                new Product() {
                    Name = "Intel Core i7-2600",
                    Price = 315
                };
            });
        }
    }
}