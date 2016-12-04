using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Models.Views;
using System.Web.Routing;

namespace ShoppingCart.Controllers {
    public class HomeController : Controller {
        protected CartModel db = new CartModel();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName != "Login" &&
                filterContext.ActionDescriptor.ActionName != "LogOut" &&
                filterContext.ActionDescriptor.ActionName != "SignUp")
            {
                if (Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                                        new RouteValueDictionary
                                        {
                                            { "controller", "Home" },
                                            { "action", "Login" }
                                        });
                }
            }
        }

        public ActionResult Index() {

            var userId = Convert.ToInt32(Session["UserId"]);
            var user = db.Users.FirstOrDefault(u => u.UserId == userId);
            if (user.UserClient)
            {
                ViewBag.ProductsJson = JsonConvert.SerializeObject(db.Products.OrderBy(x => x.Name).ToList());
                return View();
            }
            else
            {
                return RedirectToAction("OpenedOrders");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var u = db.Users.FirstOrDefault(q => q.UserName == user.UserName & q.Password == user.Password);

            if (u != null)
            {
                Session["UserId"] = u.UserId.ToString();

                if (u.UserClient)
                {
                    ViewBag.ProductsJson = JsonConvert.SerializeObject(db.Products.OrderBy(x => x.Name).ToList());
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("OpenedOrders");
                }
            }

            return View();
        }

        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Login");
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

            var userId = Convert.ToInt32(Session["UserId"]);
            var user = db.Users.FirstOrDefault(u => u.UserId == userId);

            Order order = new Order() {
                Date = DateTime.Now,
                Customer = user.Name,
                Address = user.Address,
                UserClientId = Convert.ToInt32(Session["UserId"])
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

        [HttpGet]
        public ActionResult ClientOrders()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var model = db.Orders.Where(o => o.UserClientId == userId).ToList();
            return View(model);
        }

        public ActionResult DeleteOrder(int id)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var order = db.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                var oi = db.OrderItems.Where(o => o.OrderId == order.Id).ToList();
                foreach(var orderItem in oi)
                {
                    db.OrderItems.Remove(orderItem);
                }
                var m = db.Messages.Where(o => o.OrderId == order.Id).ToList();
                foreach (var msg in m)
                {
                    db.Messages.Remove(msg);
                }
                db.Orders.Remove(order);
                db.SaveChanges();
            }

            var model = db.Orders.Where(o => o.UserClientId == userId).ToList();
            return View("ClientOrders", model);
        }

        [HttpGet]
        public ActionResult OrderDetails(int id, string before)
        {
            var items = db.OrderItems.Where(o => o.OrderId == id).ToList();
            ViewBag.Before = before;
            return View(items);
        }

        [HttpGet]
        public ActionResult CourierOrderDetails(int id)
        {
            var items = db.OrderItems.Where(o => o.OrderId == id).ToList();
            return View(items);
        }



        [HttpGet]
        public ActionResult OpenedOrders()
        {
            var model = db.Orders.Where(o => o.UserCourierId == null).ToList();
            return View(model);
        }

        public ActionResult TakeOrder(int id)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var order = db.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.UserCourierId = userId;
                db.SaveChanges();
            }
            var model = db.Orders.Where(o => o.UserCourierId == null).ToList();
            return View("OpenedOrders", model);
        }

        [HttpGet]
        public ActionResult MyOrdersToFulfill()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var model = db.Orders.Where(o => o.UserCourierId == userId).ToList();
            return View(model);
        }

        public ActionResult UntakeOrder(int id)
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                var m = db.Messages.Where(o => o.OrderId == order.Id).ToList();
                foreach (var msg in m)
                {
                    db.Messages.Remove(msg);
                }
                order.UserCourierId = null;
                db.SaveChanges();
            }
            int userId = Convert.ToInt32(Session["UserId"]);

            var model = db.Orders.Where(o => o.UserCourierId == userId).ToList();
            return View("MyOrdersToFulfill", model);
        }



        [HttpGet]
        public ActionResult Chat(int id, string before)
        {
            var m = db.Messages.Where(o => o.OrderId == id).OrderByDescending(o => o.SendTime).ToList();
            ViewBag.OrderId = id;
            ViewBag.Before = before;
            return View(m);
        }

        [HttpPost]
        public ActionResult SendMessage(string content, int orderId, string before)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var user = db.Users.FirstOrDefault(u => u.UserId == userId);

            var msg = new Message()
            {
                MessageContent = content,
                OrderId = orderId,
                SenderId = userId,
                SenderName = user.Name,
                SendTime = DateTime.Now
            };
            db.Messages.Add(msg);
            db.SaveChanges();

            var m = db.Messages.Where(o => o.OrderId == orderId).OrderByDescending(o => o.SendTime).ToList();
            ViewBag.OrderId = orderId;
            ViewBag.Before = before;
            return View("Chat", m);
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