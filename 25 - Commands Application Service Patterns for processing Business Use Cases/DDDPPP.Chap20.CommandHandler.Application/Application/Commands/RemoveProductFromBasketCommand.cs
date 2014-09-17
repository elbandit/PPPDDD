using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Commands
{
    public class RemoveProductFromBasketCommand : IBusinessRequest
    {
        public RemoveProductFromBasketCommand(Guid get_basket_id, int product_id)
        {
            this.get_basket_id = get_basket_id;
            this.product_id = product_id;
        }

        public Guid get_basket_id { get; set; }
        public int product_id { get; set; }
    }
}