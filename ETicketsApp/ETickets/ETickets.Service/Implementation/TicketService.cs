using ETickets.Domain.DTO;
using Etickets.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using ETickets.Service.Interface;
using ETickets.Domain.DomainModels;
using ETickets.Domain.Relations;
using System.Linq;

namespace ETickets.Service.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _TicketRepository;
        private readonly IRepository<TicketInShoppingCart> _TicketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public TicketService(IRepository<Ticket> TicketRepository, IRepository<TicketInShoppingCart> TicketInShoppingCartRepository, IUserRepository userRepository)
        {
            _TicketRepository = TicketRepository;
            _userRepository = userRepository;
            _TicketInShoppingCartRepository = TicketInShoppingCartRepository;
        }


        public bool AddToShoppingCart(AddToShoppingCardDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.SelectedTicketId != null && userShoppingCard != null)
            {
                var Ticket = this.GetDetailsForTicket(item.SelectedTicketId);
                //{896c1325-a1bb-4595-92d8-08da077402fc}

                if (Ticket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = Ticket,
                        TicketId = Ticket.Id,
                        UserCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    var existing = userShoppingCard.TicketInShoppingCarts.Where(z => z.ShoppingCartId == userShoppingCard.Id && z.TicketId == itemToAdd.TicketId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.Quantity += itemToAdd.Quantity;
                        this._TicketInShoppingCartRepository.Update(existing);

                    }
                    else
                    {
                        this._TicketInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewTicket(Ticket p)
        {
            this._TicketRepository.Insert(p);
        }

        public void DeleteTicket(Guid id)
        {
            var Ticket = this.GetDetailsForTicket(id);
            this._TicketRepository.Delete(Ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            return this._TicketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return this._TicketRepository.Get(id);
        }

        public AddToShoppingCardDto GetShoppingCartInfo(Guid? id)
        {
            var Ticket = this.GetDetailsForTicket(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedTicket = Ticket,
                SelectedTicketId = Ticket.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingTicket(Ticket p)
        {
            this._TicketRepository.Update(p);
        }
    }
}
