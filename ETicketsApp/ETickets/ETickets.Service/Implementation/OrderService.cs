using ETickets.Domain.DomainModels;
using ETickets.Domain;
using Etickets.Repository.Interface;
using ETickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using ETickets.Domain.DTO;

namespace ETickets.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;


        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }
        public List<Order> getOrdersForUser(string userId)
        {
            return this._orderRepository.getUserOrders(userId);
        }
        public OrderDTO getOrderDetails(Guid id)
        {
            Order order = this._orderRepository.getOrderDetails(id);
            double totalPrice = 0;
            foreach (var item in order.TicketInOrders) {
                totalPrice += item.Ticket.Price * item.Quantity;
            }
            return new OrderDTO { Order = order, TotalPrice = totalPrice };
        }
    }
}
