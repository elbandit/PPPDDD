using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDD.Chap10.ecommerce.ImplicitLogic.Model
{
    public class BasketItem
    {
        public Quantity quantity()
        {
            throw new NotImplementedException();
        }

        internal void increase_item_quantity_by(Quantity quantity)
        {
            throw new NotImplementedException();
        }
    }
}
