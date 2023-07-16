using ETickets.Domain.DomainModels;
using ETickets.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using ETickets.Domain.DTO;

namespace ETickets.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public OrderDTO getOrderDetails(Guid Id);
        public List<Order> getOrdersForUser(string userId);

        public EmailDTO getOrderEmail(Guid orderId);
    }
}