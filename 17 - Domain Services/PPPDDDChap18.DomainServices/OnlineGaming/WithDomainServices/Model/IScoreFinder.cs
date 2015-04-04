using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.OnlineGaming.WithDomainServices.Model
{
    // repository
    public interface IScoreFinder
    {
        Score FindTopScore(IGame game, int resultNumber);
    }
}
