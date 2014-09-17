using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Shopping.Commands;
using Agathas.Storefront.Shopping.Model.Baskets;

namespace Agathas.Storefront.Shopping.Handlers
{
    public class CreateABasketCommandHandler : ICommandHandler<CreateABasketCommand>
    {
        private readonly IBasketRepository _basket_repository;

        public CreateABasketCommandHandler(IBasketRepository basket_repository)
        {
            _basket_repository = basket_repository;
        }

        public void action(CreateABasketCommand business_request)
        {
            var basket = new Basket(business_request.basket_id);

            _basket_repository.add(basket);
        }
    }
}