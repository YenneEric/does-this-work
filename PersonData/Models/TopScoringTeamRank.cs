using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonData.Models
{
    public class TopScoringTeamRank
    {
        public string TeamName { get; set; }
        public int TotalPoints { get; set; }
        public int GamesPlayed { get; set; }
        public decimal AveragePoints { get; set; }
        public long TeamRank { get; set; }

        public TopScoringTeamRank(string teamName, int totalPoints, int gamesPlayed, decimal averagePoints, long teamRank)
        {
            TeamName = teamName;
            TotalPoints = totalPoints;
            GamesPlayed = gamesPlayed;
            AveragePoints = averagePoints;
            TeamRank = teamRank;
        }
    }
}