using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PPPSSS.Dist.UIComp.Controllers
{
    /*
    * This controller represents an API provided by the Search
    * Bounded Context. It would run as it's own dedicated application
    * and not live inside the same project as the other APIs
    * currently in this project
    */
    public class PromotionsController : Controller
    {
        public JsonResult Index()
        {
            var holidays = new List<Holiday>
            {
                new Holiday
                {
                    Title = "Relaxing Med Cruise",
                    Price = 999,
                    ImgUrl = "http://media.wiley.com/product_data/coverImage/84/04702927/0470292784.jpg"
                },
                new Holiday
                {
                    Title = "Romantic Weekend Break in Paris",
                    Price = 120,
                    ImgUrl = "http://media.wiley.com/product_data/coverImage/84/04702927/0470292784.jpg"
                }
            };

            return Json(holidays, JsonRequestBehavior.AllowGet);
        }

        class Holiday
        {
            public string Title { get; set; }

            public int Price { get; set; }

            public string ImgUrl { get; set; }
        }
    }
}