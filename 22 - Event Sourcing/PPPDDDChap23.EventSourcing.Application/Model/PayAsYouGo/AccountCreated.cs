using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class AccountCreated  : DomainEvent
    {
        public AccountCreated(Guid aggregateId, Money credit)
            : base(aggregateId)
        {
            Credit = credit;
        }

        public Money Credit { get; private set; }        
    }
}
