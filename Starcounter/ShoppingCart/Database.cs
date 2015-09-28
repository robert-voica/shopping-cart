using System;
using System.Linq;
using Starcounter;

namespace ShoppingCart {
    [Database]
    public class Order {
        public DateTime Date;
        public string Customer;
        public string Address;
        
        public QueryResultRows<OrderItem> OrderItems {
            get {
                return Db.SQL<OrderItem>("SELECT oi FROM ShoppingCart.OrderItem oi WHERE oi.\"Order\" = ?", this);
            }
        }

        public decimal? TotalAmount {
            get {
                return this.OrderItems.Sum(x => (decimal?)x.TotalAmount);
            }
        }
    }

    [Database]
    public class OrderItem {
        public string Product;
        public int Quantity;
        public decimal Price;
        public Order Order;
        
        public decimal TotalAmount {
            get {
                return (decimal)this.Quantity * this.Price;
            }
        }
    }

    [Database]
    public class Product {
        public string Name;
        public decimal Price;
    }
}
