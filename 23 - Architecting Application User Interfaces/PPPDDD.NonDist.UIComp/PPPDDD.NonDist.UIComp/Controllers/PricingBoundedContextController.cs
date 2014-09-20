using System;
using System.Web;
using System.Web.Mvc;

namespace PPPDDD.NonDist.UIComp.Controllers
{
    public class PricingBoundedContextController : Controller
    {
        [ChildActionOnly] // cannot be rendered as a page
        public PartialViewResult Price(string productId)
        {
            var price = PricingBoundedContext.PriceFinder.PriceFor(productId);

            /* convention will look for a partial view called:
             * /Views/PricingBoundedContext/Price.cshtml
            */
            return PartialView(price);
        }
    }
}

// would actually live in a separate project
namespace PPPDDD.NonDist.UIComp.PricingBoundedContext
{
    public static class PriceFinder
    {
        private static Lazy<Random> random = new Lazy<Random>();

        public static int PriceFor(string productId)
        {
            // simulate a database lookup
            return random.Value.Next(1, 1000);
        }
    }
}