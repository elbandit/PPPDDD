using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDPPP.Chap21.RavenDBExample.Application.Application.Queries;

namespace DDDPPP.Chap21.RavenDBExample.Application.Model.Auction
{
    public interface IAuctionRepository
    {
        void Add(Auction auction);
        Auction FindBy(Guid Id);
    }
}
