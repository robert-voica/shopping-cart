namespace ShoppingCart.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        public Message()
        {
        }

        public int MessageId { get; set; }

        [Required]
        public string MessageContent { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        public DateTime SendTime { get; set; }

        [Required]
        public int OrderId { get; set; }
    }
}

