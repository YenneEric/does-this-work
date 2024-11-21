using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonData
{
    public class SqlUpdateRepository :IUpdate
    {
        private readonly string connectionString;

        public SqlUpdateRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public void UpdatePlayer(int playerId, string playerName = null, string position = null)
        {
            if (playerId <= 0)
                throw new ArgumentException("Player ID must be a positive integer.", nameof(playerId));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.UpdatePlayer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@PlayerId", playerId);
                    command.Parameters.AddWithValue("@PlayerName", (object)playerName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Position", (object)position ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }


        public void UpdateTeam(int teamId, string teamName = null, string location = null, string mascot = null, int? confId = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.UpdateTeam", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@TeamId", teamId);
                    command.Parameters.AddWithValue("@TeamName", (object)teamName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Mascot", (object)mascot ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ConfId", (object)confId ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }



        public void UpdatePlayerStats(
             int playerStatsId,
             int? rushingYards = null,
            int? receivingYards = null,
             int? throwingYards = null,
            int? tackles = null,
             int? sacks = null,
            int? turnovers = null,
            int? interceptionsCaught = null,
            int? touchdowns = null,
              int? punts = null,
             int? fieldGoalsMade = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.UpdatePlayerStats", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@PlayerStatsId", playerStatsId);
                    command.Parameters.AddWithValue("@RushingYards", (object)rushingYards ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ReceivingYards", (object)receivingYards ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ThrowingYards", (object)throwingYards ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Tackles", (object)tackles ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Sacks", (object)sacks ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Turnovers", (object)turnovers ?? DBNull.Value);
                    command.Parameters.AddWithValue("@InterceptionsCaught", (object)interceptionsCaught ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Touchdowns", (object)touchdowns ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Punts", (object)punts ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FieldGoalsMade", (object)fieldGoalsMade ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }




        public void UpdateGame(int gameId, DateTime? date = null, string location = null, int? canceled = null)
        {
            if (gameId <= 0)
                throw new ArgumentException("Game ID must be a positive integer.", nameof(gameId));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.UpdateGame", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@Date", (object)date ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Canceled", (object)canceled ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();

                    Console.WriteLine($"Game with ID {gameId} updated successfully.");
                }
            }
        }



        public void UpdateGameTeam(int gameTeamId, int teamTypeId, int? topOfPossessionSec = null, int? score = null)
        {
            if (gameTeamId <= 0)
                throw new ArgumentException("GameTeamId must be a positive integer.", nameof(gameTeamId));

            if (teamTypeId <= 0)
                throw new ArgumentException("TeamTypeId must be a positive integer.", nameof(teamTypeId));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.UpdateGameTeam", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@GameTeamId", gameTeamId);
                    command.Parameters.AddWithValue("@TeamTypeId", teamTypeId);
                    command.Parameters.AddWithValue("@TopOfPossessionSec", (object)topOfPossessionSec ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Score", (object)score ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();

                    Console.WriteLine($"GameTeam with ID {gameTeamId} updated successfully.");
                }
            }
        }





        public void UpdateGameDetails(
            int gameId,
            string homeTeamName = null,
            int? homeScore = null,
            int? homeTimeOfPossession = null,
            string awayTeamName = null,
            int? awayScore = null,
            int? awayTimeOfPossession = null,
            string gameLocation = null,
            DateTime? gameDate = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.UpdateGameDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@HomeTeamName", (object)homeTeamName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@HomeScore", (object)homeScore ?? DBNull.Value);
                    command.Parameters.AddWithValue("@HomeTimeOfPossession", (object)homeTimeOfPossession ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AwayTeamName", (object)awayTeamName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AwayScore", (object)awayScore ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AwayTimeOfPossession", (object)awayTimeOfPossession ?? DBNull.Value);
                    command.Parameters.AddWithValue("@GameLocation", (object)gameLocation ?? DBNull.Value);
                    command.Parameters.AddWithValue("@GameDate", (object)gameDate ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }







    }
}
