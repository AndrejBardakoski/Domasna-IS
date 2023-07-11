using ETickets.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Text;
using GemBox.Document;
using System.IO;
using System.Reflection.Metadata;

namespace ETickets.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            // free licence is limited to 20 paragraphs documents
            ComponentInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;
            // so we use trial mode
            //This option will enable you to read / write entire document content, with the following limitations imposed after the first 20 paragraphs:
            //When loading a document -random parts of document text will be replaced with string TRIAL.
            //When saving a document -promotional header will be added to every document page and will replace any existing header.
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

            var orderDTO = this._orderService.getOrderDetails((Guid)id);
            if (orderDTO == null)
            {
                return NotFound();
            }
            return View(orderDTO);
        }

        public FileContentResult GeneratePDF(Guid? id)
        {
            if (id == null)
            {
                throw new Exception();
            }

            var orderDTO = this._orderService.getOrderDetails((Guid)id);
            string orderUrl = "https://localhost:44347/Order/Details/" + orderDTO.Order.Id.ToString();

            var document = DocumentModel.Load(orderUrl,LoadOptions.HtmlDefault);
            Section section = document.Sections[0];
            section.Blocks.RemoveAt(section.Blocks.Count - 1);  // remove footer
            section.Blocks.RemoveAt(section.Blocks.Count - 1);  // remove genereate PDF button

            var stream = new MemoryStream();
            document.Save(stream,SaveOptions.PdfDefault);

            var filename = "Order_" + orderDTO.Order.Id.ToString().Substring(0, 7) + "_Details.pdf";
            return File(stream.ToArray(), SaveOptions.PdfDefault.ContentType, filename);
        }
    }
}
