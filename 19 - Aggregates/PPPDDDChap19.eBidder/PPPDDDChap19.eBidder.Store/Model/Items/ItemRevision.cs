using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Store.Application.Model.Items
{
    public class ItemRevision
    {
        private Guid Item { get; set; }
        private DateTime Date { get; set; }
        private String RevisedInformation { get; set; }
    }
}
