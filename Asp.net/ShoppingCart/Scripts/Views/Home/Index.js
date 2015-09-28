(function (template) {
    var OrderItem = function () {
        this.Product = "";
        this.Price = 0;
        this.Quantity = 1;
    };

    var Order = function () {
        this.Date = new Date();
        this.Customer = "";
        this.Address = "";
        this.OrderItems = [new OrderItem()];
        this.OrderItemsCounter = 0;
    };

    var Model = function () {
        this.Order = new Order();
        this.Products = JSON.parse(document.querySelector("#scrProducts").innerHTML);
        this.Messages = [];
        this.OrderComplete = false;
    };

    template.CreateNewOrder = function () {
        template.set("model.Order", new Order());
    };

    template.DeleteOrderItem = function (e, i) {
        template.splice("model.Order.OrderItems", i, 1);
    };

    template.AddOrderItem = function () {
        template.push("model.Order.OrderItems", new OrderItem());
    };

    template.CancelOrder = function () {
        template.CreateNewOrder();
    };

    template.SubmitOrder = function () {
        template.set("model.Messages", []);
        document.querySelector("iron-ajax").generateRequest();
    };

    template.OrderSubmited = function (e, request) {
        var response = request.xhr.response;

        if (response.Success) {
            template.set("model.OrderComplete", true);
        } else {
            template.set("model.Messages", response.Messages);
        }
    };

    template.GetTotalAmount = function (price, quantity) {
        if (!price || !quantity) {
            return 0;
        }

        return price * quantity;
    };

    template.GetTotalOrderAmount = function (items) {
        var amount = 0;

        for (var i = 0, l = items.length; i < l; i++) {
            amount += template.GetTotalAmount(items[i].Price, items[i].Quantity);
        }

        return amount;
    };

    template.GetFullProductName = function (name, price) {
        return [name, " - $", price].join("");
    };

    template.ProductSelected = function (e) {
        var product = null;
        var index = e.model.index;
        var name = template.model.Order.OrderItems[index].Product;

        for (var i = 0, l = template.model.Products.length; i < l; i++) {
            var p = template.model.Products[i];

            if (p.Name == name) {
                product = p;
                break;
            }
        }

        if (product) {
            template.set("model.Order.OrderItems." + index + ".Price", product.Price);
        } else {
            template.set("model.Order.OrderItems." + index + ".Price", 0);
        }

        template.RefreshTotalAmount();
    };

    template.RefreshTotalAmount = function () {
        template.set("model.Order.OrderItemsCounter", template.model.Order.OrderItemsCounter + 1);
    }

    template.model = new Model();
})(document.currentScript.previousElementSibling);