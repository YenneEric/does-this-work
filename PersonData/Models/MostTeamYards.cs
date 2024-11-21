using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonData.Models
{
    public class MostTeamYards
    {

        public string TeamName { get; }
        public int Yards { get; }

        public long Rank { get; }
        public MostTeamYards(string teamName, int yards, long rank)
        {
            TeamName = teamName;
            Yards = yards;
            Rank = rank;
        }


    }
}