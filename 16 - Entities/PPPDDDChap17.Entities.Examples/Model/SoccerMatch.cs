using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    namespace DataFocused
    {
        // This is a poorly-designed class. It exposes only data - leading to an anaemic domain model
        // In addition, logic for calculating the winner could be erroneously duplicated
        // in other parts of the application
        public class SoccerCupMatch
        {
            public SoccerCupMatch(Guid id, Scores team1Scores, Scores team2Scores)
            {
                if (id == null)
                    throw new ArgumentNullException("Soccer cup match ID cannot be null");

                if (team1Scores == null)
                    throw new ArgumentNullException("Team 1 scores cannot be null");

                if (team2Scores == null)
                    throw new ArgumentNullException("Team 2 scores cannot be null");

                this.ID = id;
                this.Team1Scores = team1Scores;
                this.Team2Scores = team2Scores;
            }

            public Guid ID { get; private set; }

            public Scores Team1Scores { get; set; }

            public Scores Team2Scores { get; set; }
        }
    }
    
    namespace BehaviorFocused
    {
        public class SoccerCupMatch
        {
            public SoccerCupMatch(Guid id, Scores team1Scores, Scores team2Scores)
            {
                if (id == null)
                    throw new ArgumentNullException("Soccer cup match ID cannot be null");

                if (team1Scores == null)
                    throw new ArgumentNullException("Team 1 scores cannot be null");

                if (team2Scores == null)
                    throw new ArgumentNullException("Team 2 scores cannot be null");

                this.ID = id;
                this.Team1Scores = team1Scores;
                this.Team2Scores = team2Scores;
            }

            public Guid ID { get; private set; }

            private Scores Team1Scores { get; set; }

            private Scores Team2Scores { get; set; }

            public Scores WinningTeamScores
            {
                get
                {
                    if (Team1Scores.TotalScore > Team2Scores.TotalScore) 
                        return Team1Scores;

                    if (Team2Scores.TotalScore > Team1Scores.TotalScore) 
                        return Team2Scores;

                    var awayGoalsWinner = FindWinnerUsingAwayGoalsRule();
                    if (awayGoalsWinner == null)
                        return FindWinnerOfPenaltyShootout();
                    else 
                        return awayGoalsWinner;
                }
            }

            private Scores FindWinnerUsingAwayGoalsRule()
            {
                if (Team1Scores.AwayLegGoals > Team2Scores.AwayLegGoals)
                    return Team1Scores;

                if (Team2Scores.AwayLegGoals > Team1Scores.AwayLegGoals)
                    return Team2Scores;

                // The scores were exactly the same so no away goals winners
                return null;
            }

            private Scores FindWinnerOfPenaltyShootout()
            {
                if (Team1Scores.ShootoutScore > Team2Scores.ShootoutScore)
                    return Team1Scores;

                if (Team2Scores.ShootoutScore > Team1Scores.ShootoutScore)
                    return Team2Scores;

                throw new ThereWasNoPenaltyShootout();
            }
        }
    }

    public class Scores
    {
        public Scores(Guid teamId, int homeLegGoals, int awayLegGoals, int shootoutScore)
        {
            this.TeamID = teamId;
            this.HomeLegGoals = homeLegGoals;
            this.AwayLegGoals = awayLegGoals;
            this.ShootoutScore = shootoutScore;
        }

        public Guid TeamID { get; private set; }

        public int HomeLegGoals { get; private set; }

        public int AwayLegGoals { get; private set; }

        public int ShootoutScore { get; private set; }

        public int TotalScore 
        {
            get 
            {
                return HomeLegGoals + AwayLegGoals;
            }
        }
    }

    public class ThereWasNoPenaltyShootout : Exception { }

}
