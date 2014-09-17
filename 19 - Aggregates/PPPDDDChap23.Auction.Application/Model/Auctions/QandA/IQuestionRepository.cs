using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.Auction.Application.Model.Auctions.QandA
{
    public interface IQuestionRepository
    {
        Question FindBy(Guid id);
    }
}
