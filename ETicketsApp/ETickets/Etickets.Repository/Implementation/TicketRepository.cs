using Etickets.Repository.Interface;
using ETickets.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Etickets.Repository.Implementation
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Ticket> GetFilterByDate(DateTime dateBegin, DateTime dateEnd)
        {
            return entities.Where(p => p.Date >= dateBegin && p.Date <= dateEnd).ToListAsync().Result;
        }
    }
}
