using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PersonData
{
    public interface IInsert
    {

        
        int CreatePlayer(string name, string pos);


        int CreateTeamPlayer(int jerseynumber, string year, string teamname, int playerid);

        void CreatePlayerStats(
        int playerId,
        string seasonYear,
        string teamName,
        int gameId,
        int rushingYards = 0,
        int receivingYards = 0,
        int throwingYards = 0,
        int tackles = 0,
        int sacks = 0,
        int turnovers = 0,
        int interceptionsCaught = 0,
        int fieldGoalsMade = 0,
        int touchdowns = 0,
        int punts = 0
    );

        int CreateGame(DateTime date, string location, int canceled = 0);


        int CreateGameTeam(
        int gameId,
        string teamName,
        int teamTypeId,
        int? topOfPossessionSec = null,
        int? score = null);


        int CreateTeam(string teamName, string location, string mascot, string confName);


        void CreateConference(string confName);

        void CreateSeason(int year);
        




        }
}
