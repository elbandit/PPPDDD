using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap18.DomainServices
{
    public class Competitor
    {
        // ...

        public Guid Id {get; protected set;}

        public string GamerTag { get; protected set; }

        public Score Score { get; set; }

        public Ranking WorldRanking { get; set; }
    }

    /* Dubious version with hard-coded score calculation logic
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
    */

    public class OnlineDeathmatch : IGame
    {
        private Competitor player1;
        private Competitor player2;

        // Domain Services
        private IEnumerable<IGamingScorePolicy> scorers;
        private IEnumerable<IGamingRewardPolicy> rewarders;

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

        public void CommenceBattle()
        {
            // ...

            // battle completes

            // would actually be based on game result
            var winner = player1;
            var loser = player2;

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
        
    public interface IGame
    {
        // ...
    }

    // Value Objects

    public class Score
    {
        public int Value {get; private set;}

        public Score(int value)
        {
            this.Value = value;
        }

        public Score Add(Score amount)
        {
            return new Score(this.Value + amount.Value);
        }

        public Score Subtract(Score amount)
        {
            return new Score(this.Value - amount.Value);
        }
    }

    public class Ranking
    {
        // ...
    }
    
    // Domain Service interfaces - part of Ubiquitous Language
    public interface IGamingScorePolicy
    {
        void Apply(IGame game);
    }

    public interface IGamingRewardPolicy
    {
        void Apply(IGame game);
    }

    // Domain Service implementation. Part of the Ubiquitous Language
    /*
    public class Free12MonthSubscriptionForHighScoresReward : IGamingRewardPolicy
    {
        private IScoreFinder repository;

        public Free12MonthSubscriptionForHighScoresReward(IScoreFinder repository)
        {
            this.repository = repository;
        }

        public void Apply(IGame game)
        {
            var top100Threshold = repository.FindTopScore(game, 100);
            if (game.Winners.Single().Score > top100Threshold)
            {
                // trigger reward process
            }
        }
    }
     */

    // repository
    public interface IScoreFinder
    {
        Score FindTopScore(IGame game, int resultNumber);
    }
}
