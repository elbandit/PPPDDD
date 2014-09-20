using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap17.Entities.Examples
{
    namespace DataFocused
    {
        public class SoccerMatch
        {
            public SoccerMatch(Guid id)
            {
                this.Id = id;
                this.HomeTeamScore = new Score(0);
                this.AwayTeamScore = new Score(0);
                this.HomeTeamGoals = new Goals();
                this.AwayTeamGoals = new Goals();
            }

            public Guid Id { get; private set; }

            public Score HomeTeamScore { get; set; }

            public Score AwayTeamScore { get; set; }

            public Goals HomeTeamGoals { get; set; }

            public Goals AwayTeamGoals { get; set; }
        }
    }
    
    namespace BehaviorFocused
    {
        public class SoccerMatch
        {
            public SoccerMatch(Guid id)
            {
                this.Id = id;
                this.HomeTeamScore = new Score(0);
                this.AwayTeamScore = new Score(0);
                this.HomeTeamGoals = new Goals();
                this.AwayTeamGoals = new Goals();
            }

            public Guid Id { get; private set; }

            private Score HomeTeamScore { get; set; }

            private Score AwayTeamScore { get; set; }

            private Goals HomeTeamGoals { get; set; }

            private Goals AwayTeamGoals { get; set; }

            public void ScoreHomeTeamGoal(Goal goal)
            {
                HomeTeamGoals = HomeTeamGoals.Add(goal);
                HomeTeamScore = HomeTeamScore.Increment();
            }

            public void ScoreAwayTeamGoal(Goal goal)
            {
                AwayTeamGoals = AwayTeamGoals.Add(goal);
                AwayTeamScore = AwayTeamScore.Increment();
            }
        }
    }

    public class Score
    {
        public Score(int numberOfGoals)
        {
            this.NumberOfGoals = numberOfGoals;
        }

        public int NumberOfGoals { get; private set; }

        public Score Increment() 
        {
            return new Score(NumberOfGoals + 1);
        }
    }

    public class Goals
    {
        private List<Goal> goals;

        public Goals()
        {
            goals = new List<Goal>();
        }

        public Goals(IEnumerable<Goal> goals)
        {
            this.goals = goals.ToList();
        }

        public Goals Add(Goal goal)
        {
            var ng = new Goal[goals.Count + 1];
            goals.CopyTo(ng, 0);
            ng[goals.Count] = goal;

            return new Goals(ng);
        }
    }

    public class Goal
    {
        public int Minute { get; private set; }

        public string Name { get; private set; }
    }


}
