using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonData.Models
{
    public class ConferenceTeamRank
    {
        public string TeamName { get; }
        public int Wins { get; }
        public long ConferenceRank { get; }

        public ConferenceTeamRank(string teamName, int wins, long conferenceRank)
        {
            TeamName = teamName;
            Wins = wins;
            ConferenceRank = conferenceRank;
        }
    }
}