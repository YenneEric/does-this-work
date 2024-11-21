using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonData
{
    public class SqlInsertRepository : IInsert
    {
        private readonly string connectionString;

        public SqlInsertRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int CreatePlayer(string name, string pos)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Player name cannot be null or empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(pos))
                throw new ArgumentException("Position cannot be null or empty.", nameof(pos));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.CreatePlayer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PlayerName", name);
                    command.Parameters.AddWithValue("@Position", pos);

                    var playerIdParam = new SqlParameter("@PlayerId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(playerIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)playerIdParam.Value;
                }
            }

        }




        public int CreateTeamPlayer(int jerseynumber, string year, string teamname, int playerid)
        {
            if (string.IsNullOrWhiteSpace(year))
                throw new ArgumentException("Year cannot be null or empty.", nameof(year));

            if (string.IsNullOrWhiteSpace(teamname))
                throw new ArgumentException("Team name cannot be null or empty.", nameof(teamname));

            if (playerid <= 0)
                throw new ArgumentException("Player ID must be a positive integer.", nameof(playerid));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.CreateTeamPlayer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@JerseyNumber", jerseynumber);
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@TeamName", teamname);
                    command.Parameters.AddWithValue("@PlayerId", playerid);

                    var teamPlayerIdParam = new SqlParameter("@TeamPlayerId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(teamPlayerIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    int teamPlayerId = (int)teamPlayerIdParam.Value;
                    Console.WriteLine($"TeamPlayer created successfully with TeamPlayerId: {teamPlayerId} for PlayerId {playerid} with JerseyNumber {jerseynumber} in Team {teamname} for Season {year}.");

                    return teamPlayerId;
                }
            }
        }



        public void CreatePlayerStats(
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
    )
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.CreatePlayerStats", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PlayerId", playerId);
                    command.Parameters.AddWithValue("@SeasonYear", seasonYear);
                    command.Parameters.AddWithValue("@TeamName", teamName);
                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@RushingYards", rushingYards);
                    command.Parameters.AddWithValue("@ReceivingYards", receivingYards);
                    command.Parameters.AddWithValue("@ThrowingYards", throwingYards);
                    command.Parameters.AddWithValue("@Tackles", tackles);
                    command.Parameters.AddWithValue("@Sacks", sacks);
                    command.Parameters.AddWithValue("@Turnovers", turnovers);
                    command.Parameters.AddWithValue("@InterceptionsCaught", interceptionsCaught);
                    command.Parameters.AddWithValue("@FieldGoalsMade", fieldGoalsMade);
                    command.Parameters.AddWithValue("@Touchdowns", touchdowns);
                    command.Parameters.AddWithValue("@Punts", punts);


                    connection.Open();

                    command.ExecuteNonQuery();

                }
            }
        }




        public int CreateGame(DateTime date, string location, int canceled = 0)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be null or empty.", nameof(location));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.CreateGame", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@Canceled", canceled);

                    var gameIdParam = new SqlParameter("@GameId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(gameIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)gameIdParam.Value;
                }
            }
        }




        public int CreateGameTeam(
                 int gameId,
                string teamName,
                int teamTypeId, 
                int? topOfPossessionSec = null,
               int? score = null)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                throw new ArgumentException("Team name cannot be null or empty.", nameof(teamName));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.CreateGameTeam", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@TeamName", teamName);
                    command.Parameters.AddWithValue("@TeamTypeId", teamTypeId); 
                    command.Parameters.AddWithValue("@TopOfPossessionSec", (object)topOfPossessionSec ?? DBNull.Value); 
                    command.Parameters.AddWithValue("@Score", (object)score ?? DBNull.Value); 
                    var gameTeamIdParam = new SqlParameter("@GameTeamId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(gameTeamIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)gameTeamIdParam.Value;
                }
            }
        }




        public int CreateTeam(string teamName, string location, string mascot, string confName)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                throw new ArgumentException("Team name cannot be null or empty.", nameof(teamName));

            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be null or empty.", nameof(location));

            if (string.IsNullOrWhiteSpace(confName))
                throw new ArgumentException("Conference name cannot be null or empty.", nameof(confName));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.CreateTeam", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@TeamName", teamName);
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@Mascot", (object)mascot ?? DBNull.Value); 
                    command.Parameters.AddWithValue("@ConfName", confName);

                    
                    var teamIdParam = new SqlParameter("@TeamId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(teamIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    
                    return (int)teamIdParam.Value;
                }
            }
        }


        public void CreateConference(string confName)
        {
            if (string.IsNullOrWhiteSpace(confName))
                throw new ArgumentException("Conference name cannot be null or empty.", nameof(confName));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.CreateConference", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@ConfName", confName);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }




        public void CreateSeason(int year)
        {
            if (year <= 0)
                throw new ArgumentException("Year must be a positive integer.", nameof(year));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.CreateSeason", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                   
                    command.Parameters.AddWithValue("@Year", year);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }

















    }
}








