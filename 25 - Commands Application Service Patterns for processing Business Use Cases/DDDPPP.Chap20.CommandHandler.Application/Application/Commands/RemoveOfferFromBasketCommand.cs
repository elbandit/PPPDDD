using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Commands
{
    public class RemoveCouponFromBasketCommand : IBusinessRequest
    {
        private readonly string _couponCode;
        private readonly Guid _basket_id;

        public RemoveCouponFromBasketCommand(Guid basket_id, string couponCode)
        {
            _couponCode = couponCode;
            _basket_id = basket_id;
        }

        public string coupon_code
        {
            get { return _couponCode; }
        }

        public Guid basket_id
        {
            get { return _basket_id; }
        }
    }
}