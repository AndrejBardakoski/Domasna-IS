using ETickets.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Domain.Identity
{
    public class ETicketAppUser : IdentityUser
    {
        public bool IsAdmin { get; set; }

        public virtual ShoppingCart UserCart { get; set; }
    }
}
