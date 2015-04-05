using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.OnlineGaming.WithDomainServices.Model
{
    public interface IGame
    {
        IEnumerable<Competitor> Winners { get; }

        IEnumerable<Competitor> Losers { get; }
    }
}
