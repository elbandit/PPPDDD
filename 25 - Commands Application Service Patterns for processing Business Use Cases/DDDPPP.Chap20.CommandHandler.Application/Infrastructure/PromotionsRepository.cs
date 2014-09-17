using System;
using Agathas.Storefront.Infrastructure.Repositories;
using Agathas.Storefront.Shopping.Model.Promotions;

namespace Agathas.Storefront.Shopping.Infrastructure
{
    public class PromotionsRepository : NhRepository<Promotion, Guid>, IPromotionsRepository
    {
        public PromotionsRepository(ISessionCoordinator session)
            : base(session)
        {
        }
        
        public Promotion find_by(string voucher_code)
        {
            return base.query_for_single(x => x.voucher_code == voucher_code);
        }
    }
}