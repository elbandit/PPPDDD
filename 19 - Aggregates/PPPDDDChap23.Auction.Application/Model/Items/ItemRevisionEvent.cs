using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.Auction.Application.Model.Items
{
    public class ItemRevisionEvent
    {
        private Guid Item { get; set; }
        private DateTime Date {get; set;}

        private String RevisedInformation { get; set; }
    }
}
