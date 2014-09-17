using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.Items
{
    public class Item
    {
        private string Title { get; set; }
        private string Description { get; set; }
        private string Condition { get; set; }

        private decimal PostageCosts { get; set; }

        private string PaymentMethodsAccepted { get; set; }
        private string DispatchTime { get; set; }
    }
}
