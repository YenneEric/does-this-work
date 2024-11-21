using PersonData.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace PersonData
{
    public class SqlTouchDownRepository : IStatRepository
    {
        private readonly string connectionString;

        public SqlTouchDownRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }



        public IReadOnlyList<PlayerTouchdownRank> FetchTouchdownPlayer(string pos, int year)
        {
            if (string.IsNullOrWhiteSpace(pos))
                throw new ArgumentException("Position cannot be null or empty.", nameof(pos));

            if (year <= 0)
                throw new ArgumentException("Year must be a positive integer.", nameof(year));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.FetchTouchdownsRank", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("Year", year);
                    command.Parameters.AddWithValue("Position", pos);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var stats = TranslatePlayerTouchdownStats(reader);

                        if (stats.Count == 0)
                            throw new RecordNotFoundException($"No players found for position {pos} in year {year}.");

                        return stats;
                    }
                }
            }
        }

        public List<PlayerTouchdownRank> FetchTouchdownsRank(int year, string position)
        {
            var stats = new List<PlayerTouchdownRank>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("Football.FetchTouchdownsRank", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Position", position);

                    using (var reader = command.ExecuteReader())
                    {
                        stats = TranslatePlayerTouchdownStats(reader);
                    }
                }
            }

            return stats;
        }

        private List<PlayerTouchdownRank> TranslatePlayerTouchdownStats(SqlDataReader reader)
        {
            var stats = new List<PlayerTouchdownRank>();

            
            var playerIdOrdinal = reader.GetOrdinal("PlayerId");
            var playerNameOrdinal = reader.GetOrdinal("PlayerName");
            var positionOrdinal = reader.GetOrdinal("Position");
            var teamNameOrdinal = reader.GetOrdinal("TeamName");
            var totalTouchdownsOrdinal = reader.GetOrdinal("TotalTouchdowns");
            var positionRankOrdinal = reader.GetOrdinal("PositionRank");

            while (reader.Read())
            {
                var player = new Player(
                    reader.GetInt32(playerIdOrdinal),
                    reader.GetString(playerNameOrdinal),
                    reader.GetString(positionOrdinal)
                );

                var teamName = reader.GetString(teamNameOrdinal);
                var totalTouchdowns = reader.GetInt32(totalTouchdownsOrdinal);
                var positionRank = reader.GetInt64(positionRankOrdinal);

                stats.Add(new PlayerTouchdownRank(player, teamName, totalTouchdowns, positionRank));
            }

            return stats;
        }

        public IReadOnlyList<GameSchedule> FetchGameSchedule(string teamName, int year)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                throw new ArgumentException("Team name cannot be null or empty.", nameof(teamName));

            if (year <= 0)
                throw new ArgumentException("Year must be a positive integer.", nameof(year));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.GetTeamSchedule", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@TeamName", teamName);
                    command.Parameters.AddWithValue("@Year", year);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var schedule = TranslateGameSchedule(reader);

                        if (schedule.Count == 0)
                            throw new RecordNotFoundException($"No schedule found for team {teamName} in year {year}.");

                        return schedule;
                    }
                }
            }
        }

        private List<GameSchedule> TranslateGameSchedule(SqlDataReader reader)
        {
            var schedule = new List<GameSchedule>();

            
            var gameIdOrdinal = reader.GetOrdinal("GameId");
            var gameDateOrdinal = reader.GetOrdinal("GameDate");
            var gameLocationOrdinal = reader.GetOrdinal("GameLocation");
            var teamNameOrdinal = reader.GetOrdinal("Team");
            var teamScoreOrdinal = reader.GetOrdinal("TeamScore");
            var teamTimeOfPossessionOrdinal = reader.GetOrdinal("TeamTimeOfPossession");
            var opponentNameOrdinal = reader.GetOrdinal("Opponent");
            var opponentScoreOrdinal = reader.GetOrdinal("OpponentScore");
            var opponentTimeOfPossessionOrdinal = reader.GetOrdinal("OpponentTimeOfPossession");
            var winnerOrdinal = reader.GetOrdinal("Winner");

            while (reader.Read())
            {
                schedule.Add(new GameSchedule
                {
                    GameId = reader.GetInt32(gameIdOrdinal), 
                    GameDate = reader.GetDateTime(gameDateOrdinal).Date, 
                    GameLocation = reader.GetString(gameLocationOrdinal),
                    TeamName = reader.GetString(teamNameOrdinal),
                    TeamScore = reader.IsDBNull(teamScoreOrdinal) ? (int?)null : reader.GetInt32(teamScoreOrdinal),
                    TeamTimeOfPossession = reader.IsDBNull(teamTimeOfPossessionOrdinal) ? (int?)null : reader.GetInt32(teamTimeOfPossessionOrdinal),
                    OpponentName = reader.GetString(opponentNameOrdinal),
                    OpponentScore = reader.IsDBNull(opponentScoreOrdinal) ? (int?)null : reader.GetInt32(opponentScoreOrdinal),
                    OpponentTimeOfPossession = reader.IsDBNull(opponentTimeOfPossessionOrdinal) ? (int?)null : reader.GetInt32(opponentTimeOfPossessionOrdinal),
                    Winner = reader.GetString(winnerOrdinal)
                });
            }

            return schedule;
        }

        public IReadOnlyList<GamePlayerStats> FetchPlayerStatsByGameAndTeam(int gameId, string teamName, int? playerId = null)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                throw new ArgumentException("Team name cannot be null or empty.", nameof(teamName));

            if (gameId <= 0)
                throw new ArgumentException("Game ID must be a positive integer.", nameof(gameId));

            var playerStats = new List<GamePlayerStats>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.GetPlayerStatsByGameAndTeam", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                      
                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@TeamName", teamName);

                    
                    if (playerId.HasValue)
                        command.Parameters.AddWithValue("@PlayerId", playerId.Value);
                    else
                        command.Parameters.AddWithValue("@PlayerId", DBNull.Value);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playerStats.Add(new GamePlayerStats(
                                gameId,
                                reader.GetInt32(reader.GetOrdinal("PlayerId")), 
                                reader.GetString(reader.GetOrdinal("PlayerName")),
                                reader.GetString(reader.GetOrdinal("Position")),
                                reader.IsDBNull(reader.GetOrdinal("RushingYards")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RushingYards")),
                                reader.IsDBNull(reader.GetOrdinal("ReceivingYards")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ReceivingYards")),
                                reader.IsDBNull(reader.GetOrdinal("ThrowingYards")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ThrowingYards")),
                                reader.IsDBNull(reader.GetOrdinal("Tackles")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Tackles")),
                                reader.IsDBNull(reader.GetOrdinal("Sacks")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Sacks")),
                                reader.IsDBNull(reader.GetOrdinal("Turnovers")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Turnovers")),
                                reader.IsDBNull(reader.GetOrdinal("InterceptionsCaught")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("InterceptionsCaught")),
                                reader.IsDBNull(reader.GetOrdinal("Touchdowns")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Touchdowns")),
                                reader.IsDBNull(reader.GetOrdinal("Punts")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Punts")),
                                reader.IsDBNull(reader.GetOrdinal("FieldGoalsMade")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("FieldGoalsMade"))
                            ));
                        }
                    }
                }
            }

            return playerStats;
        }


        public void EditPlayerStats(
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
    int? fieldGoalsMade = null)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                throw new ArgumentException("Team name cannot be null or empty.", nameof(teamName));

            if (gameId <= 0)
                throw new ArgumentException("Game ID must be a positive integer.", nameof(gameId));

            if (playerId <= 0)
                throw new ArgumentException("Player ID must be a positive integer.", nameof(playerId));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.EditPlayerStats", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@TeamName", teamName);
                    command.Parameters.AddWithValue("@PlayerId", playerId);

                    
                    command.Parameters.AddWithValue("@RushingYards", rushingYards.HasValue ? (object)rushingYards.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@ReceivingYards", receivingYards.HasValue ? (object)receivingYards.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@ThrowingYards", throwingYards.HasValue ? (object)throwingYards.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@Tackles", tackles.HasValue ? (object)tackles.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@Sacks", sacks.HasValue ? (object)sacks.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@Turnovers", turnovers.HasValue ? (object)turnovers.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@InterceptionsCaught", interceptionsCaught.HasValue ? (object)interceptionsCaught.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@Touchdowns", touchdowns.HasValue ? (object)touchdowns.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@Punts", punts.HasValue ? (object)punts.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@FieldGoalsMade", fieldGoalsMade.HasValue ? (object)fieldGoalsMade.Value : DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }






















            }
        }












        public List<TopScoringTeamRank> FetchTopScoringTeams(int year)
        {
            if (year <= 0)
                throw new ArgumentException("Year must be a positive integer.", nameof(year));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.FetchTopScoringTeams", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("Year", year);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var teams = TranslateTopScoringTeams(reader);

                        if (teams.Count == 0)
                            throw new RecordNotFoundException($"No top scoring teams found for year {year}.");

                        return teams;
                    }
                }
            }
        }

        private List<TopScoringTeamRank> TranslateTopScoringTeams(SqlDataReader reader)
        {
            var teams = new List<TopScoringTeamRank>();

            
            var teamNameOrdinal = reader.GetOrdinal("TeamName");
            var totalPointsOrdinal = reader.GetOrdinal("TotalPoints");
            var gamesPlayedOrdinal = reader.GetOrdinal("GamesPlayed");
            var averagePointsOrdinal = reader.GetOrdinal("AveragePoints");
            var teamRankOrdinal = reader.GetOrdinal("TeamRank");

            while (reader.Read())
            {
                var teamName = reader.GetString(teamNameOrdinal);
                var totalPoints = reader.GetInt32(totalPointsOrdinal);
                var gamesPlayed = reader.GetInt32(gamesPlayedOrdinal);
                var averagePoints = reader.GetDecimal(averagePointsOrdinal);
                var teamRank = reader.GetInt64(teamRankOrdinal);

                teams.Add(new TopScoringTeamRank(teamName, totalPoints, gamesPlayed, averagePoints, teamRank));
            }

            return teams;
        }



















    }

}







