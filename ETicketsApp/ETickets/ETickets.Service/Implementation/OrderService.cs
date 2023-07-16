using ETickets.Domain.DomainModels;
using ETickets.Domain;
using Etickets.Repository.Interface;
using ETickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using ETickets.Domain.DTO;
using System.Net.Mail;

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

        public EmailDTO getOrderEmail(Guid orderId)
        {
            var order = this._orderRepository.getOrderDetails(orderId);
            EmailDTO mail = new EmailDTO();
            mail.MailTo = order.User.Email;
            mail.Subject = "Sucessfuly created order!";

            StringBuilder sb = new StringBuilder();
            var totalPrice = 0.0;
            int i = 0;

            sb.AppendLine("Your order is created. The order conatins: ");
            foreach (var item in order.TicketInOrders)
            {
                i++;
                totalPrice += item.Ticket.Price * item.Quantity;
                sb.AppendLine(i.ToString() + ". " + item.Quantity 
                    + " Tickets for the movie: " + item.Ticket.MovieName 
                    + "; Starting time: " + item.Ticket.Date 
                    + " and price of: $" + item.Ticket.Price + " per Ticket");
            }

            sb.AppendLine("Total price for your order: $" + totalPrice.ToString());

            mail.Content = sb.ToString();

            return mail;
        }
    }
}
