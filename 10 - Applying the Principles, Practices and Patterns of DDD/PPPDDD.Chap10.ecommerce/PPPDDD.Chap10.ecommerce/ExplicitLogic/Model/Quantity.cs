using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDD.Chap10.ecommerce.ExplicitLogic.Model
{
    public class Quantity
    {
        public Quantity(int p)
        {
        }

        public Quantity add(Quantity quantity)
        {
            throw new NotImplementedException();
        }

        internal bool contains_more_than(Quantity quantity_threshold)
        {
            throw new NotImplementedException();
        }
    }
}
