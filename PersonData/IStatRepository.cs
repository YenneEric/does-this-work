using PersonData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonData
{
    public interface IStatRepository
    {

        IReadOnlyList<PlayerTouchdownRank> FetchTouchdownPlayer(string pos, int year);

        IReadOnlyList<GameSchedule> FetchGameSchedule(string teamName, int year);

        IReadOnlyList<GamePlayerStats> FetchPlayerStatsByGameAndTeam(int gameId, string teamName, int? playerId = null);

        void EditPlayerStats(
   int gameId,
   string teamName,
   int playerId,
   int? rushingYards = null,
   int? receivingYards = null,
   int? throwingYards = null,
   int? tackles = null,
   int? sacks = null,
   int? turnovers = null,
   int? interceptionsCaught = null,
   int? touchdowns = null,
   int? punts = null,
   int? fieldGoalsMade = null);



        List<TopScoringTeamRank> FetchTopScoringTeams(int year);

    }



}
