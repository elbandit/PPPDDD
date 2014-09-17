using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Store.Application.Model.Sellers
{
    public interface ISellerRepository
    {
        Seller FindBy(Guid id);
    }
}
