using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDD.Chap10.ecommerce.ExplicitLogic.Model
{
    //  This example, including associated classes, accentuates making logic explicit. 
    //  It does not demonstrate coding and DDD best practices
    public class basket
    {
        private BasketItems _items;
        private OverSeasSellingPolicy _over_seas_selling_policy;

        public void add(Product product)
        {
            if (basket_contains_an_item_for(product))
            {
                var item_quantity = get_item_for(product).quantity().add(new Quantity(1));
                if (_over_seas_selling_policy.is_satisfied_by(item_quantity))
                    get_item_for(product).increase_item_quantity_by(new Quantity(1));
                else
                    throw new OverSeasSellingPolicyException(
                        string.Format(
                        "You can only purchase {0} of a single product.",
                        OverSeasSellingPolicy.quantity_threshold)
                    );
            }
            else
                _items.Add(BasketItemFactory.create_item_for(product, this));
        }

        private BasketItem get_item_for(Product product)
        {
            throw new NotImplementedException();
        }

        private bool basket_contains_an_item_for(Product product)
        {
            throw new NotImplementedException();
        }

    }
}
