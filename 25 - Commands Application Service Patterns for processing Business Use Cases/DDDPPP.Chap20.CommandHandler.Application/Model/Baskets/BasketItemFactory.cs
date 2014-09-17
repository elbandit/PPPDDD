using Agathas.Storefront.Shopping.Baskets;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public static class BasketItemFactory
    {
        public static BasketItem create_item_for(ProductSnapshot product_snapshot)
        {
            return new BasketItem(product_snapshot, new NonNegativeQuantity(1));
        }
    }
}