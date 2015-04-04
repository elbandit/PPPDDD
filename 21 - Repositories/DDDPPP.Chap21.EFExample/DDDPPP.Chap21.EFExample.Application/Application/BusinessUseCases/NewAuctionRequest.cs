using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPPP.Chap21.EFExample.Application.Application.BusinessUseCases
{
    public class NewAuctionRequest
    {
        public decimal StartingPrice { get; set; }
        public DateTime EndsAt { get; set; }
    }
}
