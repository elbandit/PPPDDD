using System;
using System.Collections.Generic;
using Agathas.Storefront.Common;
using Agathas.Storefront.Shopping.Model.Promotions;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public class BasketPricingService : IBasketPricingService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IPromotionsRepository _promotions_repository;

        public BasketPricingService(IBasketRepository basketRepository, 
                                    IPromotionsRepository promotions_repository)
        {
            _basketRepository = basketRepository;
            _promotions_repository = promotions_repository;
        }

        // Side effect free function
        public BasketPricingBreakdown calculate_total_price_for(IEnumerable<BasketItem> items, IEnumerable<Coupon> coupons)
        {
            var basketPricingBreakdown = new BasketPricingBreakdown();            
            var basket_discount = new Money();
            var coupon_issue = CouponIssues.NotApplied;

            //foreach (var coupon in coupons)
            //{                
            //    // 1. Get all coupons associted with the basket
            //    // 2. Check if it is applicable or which one wins.
            //    var promotion = _promotions_repository.find_by(coupon.code);

            //    if (promotion.is_still_active() && promotion.can_be_applied_to(basket))
            //    {
            //        basket_discount = promotion.calculate_discount_for(items);
            //        coupon_issue = CouponIssues.NoIssue;
            //    }
            //    else
            //    {
            //        coupon_issue = coupon.reason_why_cannot_be_applied_to(items);
            //    }    
            //}

            var total = new Money();
            foreach(var item in items)
            {
                total = total.add(item.line_total());
            }

            basketPricingBreakdown.basket_total = total;
            basketPricingBreakdown.discount = basket_discount;
            basketPricingBreakdown.coupon_message = coupon_issue;

            return basketPricingBreakdown;
        }
    }
}
