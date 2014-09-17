using System;
using Agathas.Storefront.Common;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Baskets.Events
{
    public class BasketPriceChanged : IDomainEvent
    {
        public BasketPriceChanged(Guid basket_id, Money cost_of_basket, Money discount)
        {
            this.basket_id = basket_id;
            CostOfBasket = cost_of_basket;
            Discount = discount;
        }

        public Guid basket_id { get; private set; }
        public Money CostOfBasket { get; private set; }
        public Money Discount { get; private set; }
    }
}