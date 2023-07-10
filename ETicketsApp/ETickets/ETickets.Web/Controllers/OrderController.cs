using ETickets.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Text;

namespace ETickets.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._orderService.getOrdersForUser(userId));
        }


        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = this._orderService.getOrderDetails((Guid)id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public IActionResult GeneratePDF(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            // to be implemented
            return NotFound();
        }
    }
}
