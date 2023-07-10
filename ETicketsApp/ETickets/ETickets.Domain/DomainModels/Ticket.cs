using ETickets.Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETickets.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public string MovieName { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Genre { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }

    }
}
