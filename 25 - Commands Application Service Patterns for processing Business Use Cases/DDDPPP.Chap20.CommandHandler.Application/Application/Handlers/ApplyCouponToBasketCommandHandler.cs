using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Shopping.Commands;
using Agathas.Storefront.Shopping.Model.Baskets;
using Agathas.Storefront.Shopping.Model.Promotions;

namespace Agathas.Storefront.Shopping.Handlers
{
    public class ApplyCouponToBasketCommandHandler : ICommandHandler<ApplyCouponToBasketCommand>
    {
        private readonly IPromotionsRepository _promotions_repository;
        private readonly IBasketRepository _basket_repository;
        private readonly IBasketPricingService _basketPricingService;


        public ApplyCouponToBasketCommandHandler(IPromotionsRepository promotions_repository,
                                                IBasketRepository basket_repository,
                                                IBasketPricingService basketPricingService)
        {
            _promotions_repository = promotions_repository;
            _basket_repository = basket_repository;
            _basketPricingService = basketPricingService;
        }

        public void action(ApplyCouponToBasketCommand business_request)
        {
            var promotion = _promotions_repository.find_by(business_request.voucher_id);
            // 1. Ensure coupon is valid, still in date
            // Throw Exception - Promo code is invalid

            // Throw Exception - Promo code is not applicable                      
            var basket = _basket_repository.find_by(business_request.basket_id);
            
            basket.apply(promotion, _basketPricingService);
            
            // THROW EXCEPTION IF Coupon is not valid

            _basket_repository.save(basket);
        }
    }
}