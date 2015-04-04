using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices.OnlineGaming.WithoutDomainServices.Model
{
    // contains hard-coded logicy for applying scores and rewards - does not use a domain service
    public class OnlineDeathmatch
    {
        private Competitor player1;
        private Competitor player2;

        public OnlineDeathmatch(Competitor player1, Competitor player2, Guid id)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.Id = id;
        }

        public Guid Id { get; protected set; }

        public void CommenceBattle()
        {
            // ...

            // battle completes

            // would actually be based on game result
            var winner = player1;
            var loser = player2;

            UpdateScoresAndRewards(winner, loser);
        }

        private void UpdateScoresAndRewards(Competitor winner, Competitor loser)
        {
            // real system uses rankings, history, bonus points, in-game actions 
            // seasonal promotions, marketing campaigns
            winner.Score = winner.Score.Add(new Score(200));
            loser.Score = loser.Score.Subtract(new Score(200));
        }
    }
}
