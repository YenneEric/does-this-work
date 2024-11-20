﻿using PersonData.Models;
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

                    // Add parameters
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

        private List<PlayerTouchdownRank> TranslatePlayerTouchdownStats(SqlDataReader reader)
        {
            var stats = new List<PlayerTouchdownRank>();

            // Get column ordinals for efficiency
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

                    // Add parameters
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

            // Get column ordinals for efficiency
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
                    GameDate = reader.GetDateTime(gameDateOrdinal),
                    GameLocation = reader.GetString(gameLocationOrdinal),
                    TeamName = reader.GetString(teamNameOrdinal),
                    TeamScore = reader.GetInt32(teamScoreOrdinal),
                    TeamTimeOfPossession = reader.GetInt32(teamTimeOfPossessionOrdinal),
                    OpponentName = reader.GetString(opponentNameOrdinal),
                    OpponentScore = reader.GetInt32(opponentScoreOrdinal),
                    OpponentTimeOfPossession = reader.GetInt32(opponentTimeOfPossessionOrdinal),
                    Winner = reader.GetString(winnerOrdinal)
                });
            }

            return schedule;
        }
    }






}
