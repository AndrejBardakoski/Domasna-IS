using ETickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteTicketFromSoppingCart(string userId, Guid TicketId);
        bool order(string userId);
    }
}