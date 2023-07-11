using ETickets.Domain.DomainModels;
using ETickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Service.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        List<Ticket> GetTicketsByDate(DateTime dateBegin,DateTime dateEnd);
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket p);
        void UpdeteExistingTicket(Ticket p);
        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
    }
}