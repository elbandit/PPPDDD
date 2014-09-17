using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.Auction.Application.Model.Members
{
    public interface IMemberRepository
    {
        Member FindBy(Guid memberId);
    }
}
