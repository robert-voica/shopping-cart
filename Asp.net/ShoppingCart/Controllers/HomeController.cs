using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Models.Views;

namespace ShoppingCart.Controllers {
    public class HomeController : Controller {
        protected CartModel db = new CartModel();

        public ActionResult Index() {
            ViewBag.ProductsJson = JsonConvert.SerializeObject(db.Products.OrderBy(x => x.Name).ToList());

            return View();
        }

        [HttpPost]
        public ActionResult Submit(OrderModel Model) {
            if (Model == null) {
                return Json(new {
                    Success = false,
                    Messages = new string[] { "Invalid order!" }
                });
            }

            List<string> messages = new List<string>();

            if (string.IsNullOrEmpty(Model.Customer)) {
                messages.Add("Please provide customer name!");
            }

            if (string.IsNullOrEmpty(Model.Address)) {
                messages.Add("Please provide shipping address!");
            }

            if (!Model.OrderItems.Any()) {
                messages.Add("Please add at least one order item!");
            } else {
                if (Model.OrderItems.Any(x => x.Product == null)) {
                    messages.Add("Please choose product for each order item!");
                }

                if (Model.OrderItems.Any(x => x.Quantity < 1)) {
                    messages.Add("Please set quantity of each order item!");
                }

                if (AnyWrongOrderItems(Model)) {
                    messages.Add("Please do not hack product prices!");
                }
            }

            if (messages.Any()) {
                return Json(new {
                    Success = false,
                    Messages = messages
                });
            }

            Order order = new Order() {
                Date = DateTime.Now,
                Customer = Model.Customer,
                Address = Model.Address
            };

            foreach (OrderItemModel item in Model.OrderItems) {
                OrderItem orderItem = new OrderItem() {
                    Product = item.Product,
                    Quantity = item.Quantity,
                    Price = item.Price
                };

                orderItem.TotalAmount = orderItem.Quantity * orderItem.Price;
                order.TotalAmount += orderItem.TotalAmount;
                order.OrderItems.Add(orderItem);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return Json(new {
                Success = true
            });
        }

        protected bool AnyWrongOrderItems(OrderModel Model) {
            return Model.OrderItems.Any(x => {
                var product = db.Products.FirstOrDefault(p => p.Name == x.Product);

                return product != null && product.Price != x.Price;
            });
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            this.db.Dispose();
        }
    }
}