using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Store.Application.Model.Auctions.QandA
{
    public interface IQuestionRepository
    {
        Question FindBy(Guid id);
    }
}
