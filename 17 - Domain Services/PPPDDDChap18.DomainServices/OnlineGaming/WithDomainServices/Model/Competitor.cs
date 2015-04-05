using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.OnlineGaming.WithDomainServices.Model
{
    public class Competitor
    {
        public Guid Id { get; protected set; }

        public string GamerTag { get; protected set; }

        public Score Score { get; set; }

        public Ranking WorldRanking { get; set; }
    }
}
