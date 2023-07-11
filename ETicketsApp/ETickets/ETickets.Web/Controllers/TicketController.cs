using ETickets.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System;
using ETickets.Service.Interface;
using ETickets.Domain.DomainModels;

namespace ETickets.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _TicketService;

        public TicketController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        // GET: Tickets
        public IActionResult Index()
        {
            return View(this._TicketService.GetAllTickets());
        }

        // GET: Tickets/Details/id
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ticket = this._TicketService.GetDetailsForTicket(id);
            if (Ticket == null)
            {
                return NotFound();
            }
            return View(Ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Date,Price,MovieName,Image,Genre")] Ticket Ticket)
        {
            if (ModelState.IsValid)
            {
                Ticket.Id = Guid.NewGuid();
                this._TicketService.CreateNewTicket(Ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(Ticket);
        }

        // GET: Tickets/Edit/id
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ticket = this._TicketService.GetDetailsForTicket(id);
            if (Ticket == null)
            {
                return NotFound();
            }
            return View(Ticket);
        }

        // POST: Tickets/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Date,Price,MovieName,Image,Genre")] Ticket Ticket)
        {
            if (id != Ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._TicketService.UpdeteExistingTicket(Ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(Ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Ticket);
        }

        // GET: Tickets/Delete/id
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ticket = this._TicketService.GetDetailsForTicket(id);
            if (Ticket == null)
            {
                return NotFound();
            }

            return View(Ticket);
        }

        // POST: Tickets/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._TicketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult AddTicketToCard(Guid id)
        {
            var result = this._TicketService.GetShoppingCartInfo(id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTicketToCard(AddToShoppingCardDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._TicketService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Ticket");
            }
            return View(model);
        }
        private bool TicketExists(Guid id)
        {
            return this._TicketService.GetDetailsForTicket(id) != null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterByDate(DateTime dateBegin,DateTime dateEnd)
        {
            return View("Index", this._TicketService.GetTicketsByDate(dateBegin, dateEnd));
        }
    }
}