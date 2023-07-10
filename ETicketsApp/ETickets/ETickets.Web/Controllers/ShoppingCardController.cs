using ETickets.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace ETickets.Web.Controllers
{
    public class ShoppingCardController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCardController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
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

        public IActionResult Order(string stripeEmail=null, string stripeToken=null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.PayOrder(stripeEmail, stripeToken))
                throw new Exception();
            this.SendEmail();

            if(this._shoppingCartService.order(userId))
                return RedirectToAction("Index", "ShoppingCard");
            throw new Exception();
        }
        public bool SendEmail()
        {
            // to be implemented
            return true;
        }
        public bool PayOrder(string stripeEmail, string stripeToken)
        {
            //var customerService = new CustomerService();
            //var chargeService = new ChargeService();
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var order = this._shoppingCartService.getShoppingCartInfo(userId);

            //var customer = customerService.Create(new CustomerCreateOptions
            //{
            //    Email = stripeEmail,
            //    Source = stripeToken
            //});

            //var charge = chargeService.Create(new ChargeCreateOptions
            //{
            //    Amount = (Convert.ToInt32(order.TotalPrice) * 100),
            //    Description = "EShop Application Payment",
            //    Currency = "usd",
            //    Customer = customer.Id
            //});

            //if (charge.Status == "succeeded")
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}


            // to be implemented
            return true;
        }
    }
}
