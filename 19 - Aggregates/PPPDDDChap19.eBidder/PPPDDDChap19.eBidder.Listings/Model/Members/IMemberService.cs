using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.Members
{
    public interface IMemberService
    {
        Member GetMember(Guid memberId);
    }
}
