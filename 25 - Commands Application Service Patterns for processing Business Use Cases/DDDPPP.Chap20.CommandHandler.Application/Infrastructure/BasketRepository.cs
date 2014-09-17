using System;
using Agathas.Storefront.Infrastructure.Repositories;
using Agathas.Storefront.Shopping.Baskets;
using Agathas.Storefront.Shopping.Model.Baskets;

namespace Agathas.Storefront.Shopping.Infrastructure
{
    public class BasketRepository : NhRepository<Basket, Guid>,  IBasketRepository
    {
        public BasketRepository(ISessionCoordinator session)
            : base(session)
        {
        }
    }
}