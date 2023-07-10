using ETickets.Domain.Identity;
using ETickets.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual ETicketAppUser Owner { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }

    }
}
