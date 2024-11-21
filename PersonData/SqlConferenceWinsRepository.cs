using PersonData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PersonData
{
    public class SqlConferenceWinsRepository : IConferenceRepository
    {
        private readonly string connectionString;

        public SqlConferenceWinsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<ConferenceTeamRank> FetchConferenceTeamRank(int year, string confName)
        {
            if (string.IsNullOrWhiteSpace(confName))
                throw new ArgumentException("Conference name cannot be null or empty.", nameof(confName));

            if (year <= 0)
                throw new ArgumentException("Year must be a positive integer.", nameof(year));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.FetchConferenceTeamRank", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@ConfName", confName);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var teams = TranslateConferenceTeamStats(reader);
                        return teams.Count > 0
                            ? teams
                            : throw new RecordNotFoundException($"No teams found for conference '{confName}' in year {year}.");
                    }
                }
            }
        }

        private List<ConferenceTeamRank> TranslateConferenceTeamStats(SqlDataReader reader)
        {
            var teams = new List<ConferenceTeamRank>();

            
            var teamNameOrdinal = reader.GetOrdinal("TeamName");
            var winsOrdinal = reader.GetOrdinal("Wins");
            var conferenceRankOrdinal = reader.GetOrdinal("ConferenceRank");

            while (reader.Read())
            {
                var teamName = reader.GetString(teamNameOrdinal);
                var wins = reader.GetInt32(winsOrdinal);
                var conferenceRank = reader.GetInt32(conferenceRankOrdinal);

                teams.Add(new ConferenceTeamRank(teamName, wins, conferenceRank));
            }

            return teams;
        }
    }
}