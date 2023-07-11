using ETickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etickets.Repository.Interface
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        IEnumerable<Ticket> GetFilterByDate(DateTime dateBegin, DateTime dateEnd);
    }
}
