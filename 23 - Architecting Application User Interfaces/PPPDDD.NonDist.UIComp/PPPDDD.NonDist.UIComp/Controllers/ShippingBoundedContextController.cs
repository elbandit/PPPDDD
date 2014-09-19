using System;
using System.Web;
using System.Web.Mvc;

namespace PPPDDD.NonDist.UIComp.Controllers
{
    public class ShippingBoundedContextController : Controller
    {
        [ChildActionOnly] // cannot be rendered as a page
        public PartialViewResult DeliveryOptions()
        {
            var options = ShippingBoundedContext.DeliveryOptions.All();

            /* convention will look for a partial view called:
            * /Views/ShippingBoundedContext/DeliveryOptions.cshtml
           */
            return PartialView(options);
        }
    }
}

// would actually be in a separate project
namespace PPPDDD.NonDist.UIComp.ShippingBoundedContext
{
    using System.Collections.Generic;

    public static class DeliveryOptions
    {
        public static IEnumerable<DeliveryOption> All()
        {
            // simulate database lookup
            return new List<DeliveryOption>
            {
                new DeliveryOption
                {
                    ID = "ss1",
                    Name = "Cheap & Cheerful",
                    Price = 2,
                    Duration = new Tuple<int,int>(7, 14)
                },
                new DeliveryOption
                {
                    ID = "ss2",
                    Name = "Super Fast",
                    Price = 50,
                    Duration = new Tuple<int,int>(1, 2)
                }
            };
        }
    }

    public class DeliveryOption
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public Tuple<int, int> Duration { get; set; }
    }
}