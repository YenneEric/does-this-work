using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonData.Models;

namespace PersonData
{
    public class SqlSelectRepository: ISelect
    {
        private readonly string connectionString;

        public SqlSelectRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }



        public List<Player> GetPlayers(int? playerId = null, string position = null)
        {
            var players = new List<Player>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.GetPlayer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@PlayerId", (object)playerId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Position", (object)position ?? DBNull.Value);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            players.Add(new Player(
                                reader.GetInt32(reader.GetOrdinal("PlayerId")),
                                reader.GetString(reader.GetOrdinal("PlayerName")),
                                reader.GetString(reader.GetOrdinal("Position")
                                )
                               
                            ));
                        }
                    }
                }
            }

            return players;
        }


        public List<TeamPlayer> GetTeamPlayers(int? playerId = null, int? seasonId = null, int? teamId = null, int? jerseyNumber = null)
        {
            var teamPlayers = new List<TeamPlayer>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Football.GetTeamPlayer", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PlayerId", (object)playerId ?? DBNull.Value);
                command.Parameters.AddWithValue("@SeasonId", (object)seasonId ?? DBNull.Value);
                command.Parameters.AddWithValue("@TeamId", (object)teamId ?? DBNull.Value);
                command.Parameters.AddWithValue("@JerseyNumber", (object)jerseyNumber ?? DBNull.Value);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teamPlayers.Add(new TeamPlayer(
                            reader.GetInt32(reader.GetOrdinal("TeamPlayerId")),
                            reader.GetInt32(reader.GetOrdinal("PlayerId")),
                            reader.GetInt32(reader.GetOrdinal("SeasonId")),
                            reader.GetInt32(reader.GetOrdinal("TeamId")),
                            reader.GetInt32(reader.GetOrdinal("JerseyNumber"))
                        ));
                    }
                }
            }

            return teamPlayers;
        }




        public List<PlayerStats> GetPlayerStats(
            int? playerStatsId = null,
            int? teamPlayerId = null,
            int? gameId = null,
            int? teamId = null,
            int? rushingYards = null,
            int? receivingYards = null,
            int? throwingYards = null,
            int? tackles = null,
            int? sacks = null,
            int? turnovers = null,
            int? interceptionsCaught = null,
            int? touchdowns = null,
            int? fieldGoalsMade = null,
            int? punts = null)
        {
            var playerStatsList = new List<PlayerStats>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Football.GetPlayerStats", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                
                command.Parameters.AddWithValue("@PlayerStatsId", (object)playerStatsId ?? DBNull.Value);
                command.Parameters.AddWithValue("@TeamPlayerId", (object)teamPlayerId ?? DBNull.Value);
                command.Parameters.AddWithValue("@GameId", (object)gameId ?? DBNull.Value);
                command.Parameters.AddWithValue("@TeamId", (object)teamId ?? DBNull.Value);
                command.Parameters.AddWithValue("@RushingYards", (object)rushingYards ?? DBNull.Value);
                command.Parameters.AddWithValue("@ReceivingYards", (object)receivingYards ?? DBNull.Value);
                command.Parameters.AddWithValue("@ThrowingYards", (object)throwingYards ?? DBNull.Value);
                command.Parameters.AddWithValue("@Tackles", (object)tackles ?? DBNull.Value);
                command.Parameters.AddWithValue("@Sacks", (object)sacks ?? DBNull.Value);
                command.Parameters.AddWithValue("@Turnovers", (object)turnovers ?? DBNull.Value);
                command.Parameters.AddWithValue("@InterceptionsCaught", (object)interceptionsCaught ?? DBNull.Value);
                command.Parameters.AddWithValue("@Touchdowns", (object)touchdowns ?? DBNull.Value);
                command.Parameters.AddWithValue("@FieldGoalsMade", (object)fieldGoalsMade ?? DBNull.Value);
                command.Parameters.AddWithValue("@Punts", (object)punts ?? DBNull.Value);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        playerStatsList.Add(new PlayerStats(
                            reader.GetInt32(reader.GetOrdinal("PlayerStatsId")),
                            reader.GetInt32(reader.GetOrdinal("TeamPlayerId")),
                            reader.GetInt32(reader.GetOrdinal("GameId")),
                            reader.GetInt32(reader.GetOrdinal("TeamId")),
                            reader.GetInt32(reader.GetOrdinal("RushingYards")),
                            reader.GetInt32(reader.GetOrdinal("ReceivingYards")),
                            reader.GetInt32(reader.GetOrdinal("ThrowingYards")),
                            reader.GetInt32(reader.GetOrdinal("Tackles")),
                            reader.GetInt32(reader.GetOrdinal("Sacks")),
                            reader.GetInt32(reader.GetOrdinal("Turnovers")),
                            reader.GetInt32(reader.GetOrdinal("InterceptionsCaught")),
                            reader.GetInt32(reader.GetOrdinal("Touchdowns")),
                            reader.GetInt32(reader.GetOrdinal("FieldGoalsMade")),
                            reader.GetInt32(reader.GetOrdinal("Punts"))
                        ));
                    }
                }
            }

            return playerStatsList;
        }






        public List<GameTeam> GetGameTeams(
            int? gameTeamId = null,
            int? teamId = null,
            int? gameId = null,
            int? teamTypeId = null,
            int? topOfPossessionSec = null,
            int? score = null)
        {
            var gameTeams = new List<GameTeam>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Football.GetGameTeam", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                
                command.Parameters.AddWithValue("@GameTeamId", (object)gameTeamId ?? DBNull.Value);
                command.Parameters.AddWithValue("@TeamId", (object)teamId ?? DBNull.Value);
                command.Parameters.AddWithValue("@GameId", (object)gameId ?? DBNull.Value);
                command.Parameters.AddWithValue("@TeamTypeId", (object)teamTypeId ?? DBNull.Value);
                command.Parameters.AddWithValue("@TopOfPossessionSec", (object)topOfPossessionSec ?? DBNull.Value);
                command.Parameters.AddWithValue("@Score", (object)score ?? DBNull.Value);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gameTeams.Add(new GameTeam(
     reader.GetInt32(reader.GetOrdinal("GameTeamId")),
     reader.GetInt32(reader.GetOrdinal("TeamId")),
     reader.GetInt32(reader.GetOrdinal("GameId")),
     reader.GetInt32(reader.GetOrdinal("TeamTypeId")),
     reader.IsDBNull(reader.GetOrdinal("TopOfPossessionSec")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TopOfPossessionSec")),
     reader.IsDBNull(reader.GetOrdinal("Score")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Score"))
 ));
                    }
                }
            }

            return gameTeams;
        }




















        public List<TeamType> GetTeamTypes(int? teamTypeId = null, string name = null)
        {
            var teamTypes = new List<TeamType>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Football.GetTeamType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@TeamTypeId", (object)teamTypeId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Name", (object)name ?? DBNull.Value);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teamTypes.Add(new TeamType(
                            reader.GetInt32(reader.GetOrdinal("TeamTypeId")),
                            reader.GetString(reader.GetOrdinal("Name"))
                        ));
                    }
                }
            }

            return teamTypes;
        }








        public List<Game> GetGames(int? gameId = null, string location = null, bool? canceled = null)
        {
            var games = new List<Game>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Football.GetGame", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@GameId", (object)gameId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
                command.Parameters.AddWithValue("@Canceled", (object)(canceled.HasValue ? (canceled.Value ? 1 : 0) : (int?)null) ?? DBNull.Value);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new Game(
                            reader.GetInt32(reader.GetOrdinal("GameId")),
                            reader.GetString(reader.GetOrdinal("Location")),
                            reader.GetInt32(reader.GetOrdinal("Canceled")) == 1
                        ));
                    }
                }
            }

            return games;
        }







        public List<Team> GetTeams(int? teamId = null, string teamName = null, string location = null, string mascot = null, int? confId = null)
        {
            var teams = new List<Team>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Football.GetTeam", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@TeamId", (object)teamId ?? DBNull.Value);
                command.Parameters.AddWithValue("@TeamName", (object)teamName ?? DBNull.Value);
                command.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
                command.Parameters.AddWithValue("@Mascot", (object)mascot ?? DBNull.Value);
                command.Parameters.AddWithValue("@ConfId", (object)confId ?? DBNull.Value);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teams.Add(new Team(
                            reader.GetInt32(reader.GetOrdinal("TeamId")),
                            reader.GetString(reader.GetOrdinal("TeamName")),
                            reader.GetString(reader.GetOrdinal("Location")),
                            reader.IsDBNull(reader.GetOrdinal("Mascot")) ? null : reader.GetString(reader.GetOrdinal("Mascot")),
                            reader.GetInt32(reader.GetOrdinal("ConfId"))
                        ));
                    }
                }
            }

            return teams;
        }





        public List<Conference> GetConferences(int? confId = null, string confName = null)
        {
            var conferences = new List<Conference>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Football.GetConference", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@ConfId", (object)confId ?? DBNull.Value);
                command.Parameters.AddWithValue("@ConfName", (object)confName ?? DBNull.Value);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        conferences.Add(new Conference(
                            reader.GetInt32(reader.GetOrdinal("ConfId")),
                            reader.GetString(reader.GetOrdinal("ConfName"))
                        ));
                    }
                }
            }

            return conferences;
        }





        public List<Season> GetSeasons(int? seasonId = null, int? year = null)
        {
            var seasons = new List<Season>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Football.GetSeason", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@SeasonId", (object)seasonId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Year", (object)year ?? DBNull.Value);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seasons.Add(new Season(
                            reader.GetInt32(reader.GetOrdinal("SeasonId")),
                            reader.GetInt32(reader.GetOrdinal("Year"))
                        ));
                    }
                }
            }

            return seasons;
        }



    }



}

