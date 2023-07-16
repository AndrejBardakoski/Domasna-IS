using ETickets.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Stripe;
using System.Threading.Tasks;

namespace ETickets.Web.Controllers
{
    public class ShoppingCardController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;



        public ShoppingCardController(IShoppingCartService shoppingCartService, IOrderService orderService, IEmailService emailService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.getShoppingCartInfo(userId));
        }

        public IActionResult DeleteFromShoppingCart(Guid id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.deleteTicketFromSoppingCart(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCard");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCard");
            }
        }

        public async Task<IActionResult> OrderAsync(string stripeEmail=null, string stripeToken=null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.PayOrder(stripeEmail, stripeToken))
                throw new Exception();

            var orderId = this._shoppingCartService.order(userId);

            this.SendEmailAsync(orderId);

            return RedirectToAction("Index", "ShoppingCard");
        }
        private void SendEmailAsync(Guid orderId)
        {
            var mail = _orderService.getOrderEmail(orderId);
            _emailService.SendEmailAsync(mail);
        }
        private bool PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = ((long)(order.TotalPrice * 100)),
                Description = "ETicket Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            return charge.Status == "succeeded";
        }
    }
}
