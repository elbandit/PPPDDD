using System;
using Agathas.Storefront.Infrastructure;

namespace Agathas.Storefront.Shopping.Model.Promotions
{
    public interface IPromotionsRepository : IRepository<Promotion, Guid>
    {
        Promotion find_by(string voucher_code);
    }
}