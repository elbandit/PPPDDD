using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDD.Chap10.ecommerce.ExplicitLogic.Model
{
    public class OverSeasSellingPolicy
    {
        public static Quantity quantity_threshold = new Quantity(50);
        
        public bool is_satisfied_by(Quantity item_quantity, Country country)
        {
            if (item_quantity.contains_more_than(quantity_threshold))
                return false;
            else
                return true;
        }

        internal bool is_satisfied_by(Quantity item_quantity)
        {
            throw new NotImplementedException();
        }
    }
}
