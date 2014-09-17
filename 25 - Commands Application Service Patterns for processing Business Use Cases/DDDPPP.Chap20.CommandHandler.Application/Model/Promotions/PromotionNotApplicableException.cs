using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Model.Promotions
{
    public class PromotionNotApplicableException : DomainException
    {
        public PromotionNotApplicableException(string message_for_customer) : base(message_for_customer)
        {
        }
    }
}
