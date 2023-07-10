using ETickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Domain.DTO
{
    public class AddToShoppingCardDto
    {
        public Ticket SelectedTicket { get; set; }
        public Guid SelectedTicketId { get; set; }
        public int Quantity { get; set; }
    }
}
