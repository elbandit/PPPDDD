using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Commands
{
    public class CreateABasketCommand : IBusinessRequest
    {
        public Guid basket_id { get; set; }

        public CreateABasketCommand(Guid basket_id)
        {
            this.basket_id = basket_id;
        }
    }
}