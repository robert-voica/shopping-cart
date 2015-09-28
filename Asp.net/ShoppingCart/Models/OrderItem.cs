namespace ShoppingCart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual Order Order { get; set; }
    }
}
