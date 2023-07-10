using ETickets.Domain.Identity;
using ETickets.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ETicketAppUser User { get; set; }

        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}
