using ETickets.Domain.DomainModels;
using ETickets.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Domain.DTO
{
    public class OrderDTO
    {
        public Order Order { get; set; }

        public double TotalPrice { get; set; }
    }
}
