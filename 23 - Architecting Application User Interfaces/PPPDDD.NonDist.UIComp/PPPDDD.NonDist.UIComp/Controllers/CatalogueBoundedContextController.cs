using System;
using System.Web;
using System.Web.Mvc;

namespace PPPDDD.NonDist.UIComp.Controllers
{
    public class CatalogBoundedContextController : Controller
    {
        [ChildActionOnly] // action cannot be rendered as an individual page
        public PartialViewResult ItemInBasket(string productId)
        {
            var product = SalesBoundedContext.ProductFinder.Find(productId);

            /* convention will look for a partial view called:
             * /Views/CatalogBoundedContext/ItemInBasket.cshtml
            */
            return PartialView(product);
        }
    }
}

// This would actually be inside a separate project
namespace PPPDDD.NonDist.UIComp.SalesBoundedContext
{
    public static class ProductFinder
    {
        public static Product Find(string productId)
        {
            // simulate a database lookup
            return new Product
            {
                ID = productId,
                Name = "Product_" + productId,
                Description = "Lorem ipsum dolor sit amet",
                ImageUrl = "http://media.wiley.com/product_data/coverImage/84/04702927/0470292784.jpg"
            };
        }
    }

    public class Product
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}