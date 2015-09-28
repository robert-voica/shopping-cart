using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models.Views {
    public class OrderModel {
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public OrderItemModel[] OrderItems { get; set; }
    }
}