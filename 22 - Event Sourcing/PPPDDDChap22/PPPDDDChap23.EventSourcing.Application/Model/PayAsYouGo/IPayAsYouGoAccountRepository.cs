using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public interface IPayAsYouGoAccountRepository
    {
        PayAsYouGoAccount FindBy(Guid id);

        void Add(PayAsYouGoAccount payAsYouGoAccount);

        void Save(PayAsYouGoAccount payAsYouGoAccount); 
    }
}
