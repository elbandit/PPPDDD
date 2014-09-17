using System;
using Agathas.Storefront.Infrastructure;

namespace DDDPPP.Chap20.CommandHandler.Application
{
    public class AddProductToBasketCommand : IBusinessRequest
    {
        private readonly int _productid;
        private readonly Guid _basket_id;

        public AddProductToBasketCommand(int productid, Guid basket_id)
        {
            _productid = productid;
            _basket_id = basket_id;
        }

        public int productid
        {
            get { return _productid; }
        }

        public Guid basket_id
        {
            get { return _basket_id; }
        }
    }
}