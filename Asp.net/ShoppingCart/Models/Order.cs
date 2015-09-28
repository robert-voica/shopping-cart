namespace ShoppingCart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(250)]
        public string Customer { get; set; }

        [Required]
        public string Address { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
