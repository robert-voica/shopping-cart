namespace ShoppingCart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            //Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool UserClient { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }


        //public virtual ICollection<Order> OrdersClient { get; set; }
        //public virtual ICollection<Order> OrdersCourier { get; set; }

    }
}

