using Sales.Messages.Commands;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDDesign.Web.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Place(string userId, string productIds, string shippingTypeId)
        {
            var realProductIds = productIds.Split(',');
            var placeOrderCommand = new PlaceOrder
            {
                UserId = userId,
                ProductIds = realProductIds,
                ShippingTypeId = shippingTypeId,
                TimeStamp = DateTime.Now
            };
            MvcApplication.Bus.Send("Sales.Orders.OrderCreated", placeOrderCommand);

            return Content("Your order has been placed. You will receive email confirmation shortly.");
        }

    }
}
