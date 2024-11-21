using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonData.Models;
using static PersonData.Models.MostTeamYards;

namespace PersonData
{
    public class SqlMostTeamYards : ITeamYardsRepository
    {

        private readonly string connectionString;

        public SqlMostTeamYards(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<MostTeamYards> FetchMostTeamYards(int year)
        {
            if (year <= 0)
                throw new ArgumentException("Year must be a positive integer.", nameof(year));

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Football.FetchMostTeamYards", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter for the year
                    command.Parameters.AddWithValue("Year", year);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var teams = TranslateTeamYardsStats(reader);

                        if (teams.Count == 0)
                            throw new RecordNotFoundException($"No teams found for year {year}.");

                        return teams;
                    }
                }
            }
        }

        private List<MostTeamYards> TranslateTeamYardsStats(SqlDataReader reader)
        {
            var teams = new List<MostTeamYards>();

            // Get column ordinals for efficiency
            var teamNameOrdinal = reader.GetOrdinal("TeamName");
            var yardsOrdinal = reader.GetOrdinal("Yards");
            var rankOrdinal = reader.GetOrdinal("Rank");

            while (reader.Read())
            {
                var teamName = reader.GetString(teamNameOrdinal);
                var yards = reader.GetInt32(yardsOrdinal);
                var rank = reader.GetInt64(rankOrdinal);

                teams.Add(new MostTeamYards(teamName, yards, rank));
            }

            // Rank the teams by yards (descending order)
            teams.Sort((t1, t2) => t2.Yards.CompareTo(t1.Yards));

            return teams;
        }
    }
}