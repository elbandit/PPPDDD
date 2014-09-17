using System;
using System.Collections.Generic;
using Agathas.Storefront.Shopping.Model.Promotions;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public interface IBasketPricingService
    {
        BasketPricingBreakdown calculate_total_price_for(IEnumerable<BasketItem> items, IEnumerable<Coupon> coupons );
    }
}