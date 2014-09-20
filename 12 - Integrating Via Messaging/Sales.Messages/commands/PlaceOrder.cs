using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Messages.commands
{
    public class PlaceOrder
    {
        public string UserId { get; set; }

        public List<string> ProductIds { get; set; }

        public string ShippingTypeId { get; set;}

        public DateTime TimeStamp { get; set; }
    }
}
