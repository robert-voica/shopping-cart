using System;
using System.Linq;
using System.Collections.Generic;
using Starcounter;

namespace ShoppingCart {
    partial class IndexPage : Page {
        public void CreateNewOrder() {
            this.OrderComplete = false;
            this.Messages.Clear();

            this.Order.Data = new Order() {
                Date = DateTime.Now
            };

            this.NewOrderItem();
        }

        public void NewOrderItem() {
            new OrderItem() {
                Order = this.Order.Data,
                Quantity = 1
            };
        }

        void Handle(Input.AddOrderItemClick Action) {
            this.NewOrderItem();
        }

        void Handle(Input.CancelOrderClick Action) {
            this.CreateNewOrder();
        }

        void Handle(Input.NewOrderClick Action) {
            this.CreateNewOrder();
        }

        void Handle(Input.SubmitOrderClick Action) {
            this.Messages.Clear();

            if (string.IsNullOrEmpty(this.Order.Customer)) {
                this.Messages.Add().StringValue = "Please provide customer name!";
            }

            if (string.IsNullOrEmpty(this.Order.Address)) {
                this.Messages.Add().StringValue = "Please provide shipping address!";
            }

            if (this.Order.OrderItems.Any(x => x.Product == null)) {
                this.Messages.Add().StringValue = "Please choose product for each order item!";
            }

            if (this.Order.OrderItems.Any(x => x.Quantity < 1)) {
                this.Messages.Add().StringValue = "Please set quantity of each order item!";
            }

            if (!this.Order.OrderItems.Any()) {
                this.Messages.Add().StringValue = "Please add at least one order item!";
            }

            if (this.Messages.Any()) {
                return;
            }

            this.Transaction.Commit();
            this.OrderComplete = true;
        }

        [IndexPage_json.Order]
        public partial class IndexPageOrder : Json, IBound<Order> { 
        }

        [IndexPage_json.Products]
        public partial class IndexPageProduct : Json, IBound<Product> { 
        }

        [IndexPage_json.Order.OrderItems]
        public partial class IndexPageOrderItems : Json, IBound<OrderItem> {
            void Handle(Input.Product Action) {
                Product p = Db.SQL<Product>("SELECT p FROM ShoppingCart.Product p WHERE p.Name = ?", Action.Value).First;

                if (p != null) {
                    this.Product = p.Name;
                    this.Price = p.Price;
                } else {
                    this.Product = null;
                    this.Price = 0;
                }
            }

            void Handle(Input.DeleteClick Action) {
                this.Data.Delete();
            }
        }
    }
}
