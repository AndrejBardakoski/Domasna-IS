using ETickets.Domain.DomainModels;
using ETickets.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Etickets.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public List<Order> getUserOrders(string userId);
        public Order getOrderDetails(Guid Id);

    }
}