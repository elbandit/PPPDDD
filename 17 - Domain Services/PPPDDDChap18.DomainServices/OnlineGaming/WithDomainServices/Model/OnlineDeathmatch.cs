using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.OnlineGaming.WithDomainServices.Model
{
    public class OnlineDeathmatch : IGame
    {
        private Competitor player1;
        private Competitor player2;

        // Domain Services
        private IEnumerable<IGamingScorePolicy> scorers;
        private IEnumerable<IGamingRewardPolicy> rewarders;

        private List<Competitor> winners = new List<Competitor>();
        private IList<Competitor> losers = new List<Competitor>();

        public OnlineDeathmatch(Competitor player1, Competitor player2, Guid id,
            IEnumerable<IGamingScorePolicy> scorers, IEnumerable<IGamingRewardPolicy> rewarders)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.Id = id;
            this.scorers = scorers;
            this.rewarders = rewarders;
        }

        public Guid Id { get; protected set; }

        public IEnumerable<Competitor> Winners { get { return winners; } }

        public IEnumerable<Competitor> Losers { get { return losers; } }

        public void CommenceBattle()
        {
            // ...

            // battle completes

            // would actually be based on game result
            this.winners.Add(player1);
            this.losers.Add(player2);

            UpdateScoresAndRewards();
        }

        private void UpdateScoresAndRewards()
        {
            foreach (var scorer in scorers)
            {
                scorer.Apply(this);
            }

            foreach (var rewarder in rewarders)
            {
                rewarder.Apply(this);
            }
        }
    }
}
