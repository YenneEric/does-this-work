using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PersonData;
using PersonData.Models;

namespace View
{
    public partial class InsertData : UserControl
    {
        private readonly IStatRepository _repository;
        private readonly ISelect _selectRepository;

        public event EventHandler? AddGame;
        public event EventHandler? EditPlayer;
        public event EventHandler? EditGame;
        public event EventHandler? ViewStats;
        public event EventHandler? EditStats;

        private string? _currentTeamName;
        private int? _currentYear;

        // Properties for binding
        public List<PlayerDetails> PlayerDetails { get; set; } = new List<PlayerDetails>();
        public List<GameSchedule> ScheduleDetails { get; set; } = new List<GameSchedule>();
        public List<string> Teams { get; set; } = new List<string>();
        public List<int> Years { get; set; } = new List<int>();
        public string TeamInfo { get; set; } = string.Empty;

        public event EventHandler<RoutedEventArgs>? CustomChange;
        public event EventHandler? AddPlayer;

        public InsertData()
        {
            InitializeComponent();

            const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";
            _repository = new SqlTouchDownRepository(connectionString);
            _selectRepository = new SqlSelectRepository(connectionString);

            LoadTeamsAndYears();
            ClearData();
        }

        // Load teams and years into dropdowns
        private void LoadTeamsAndYears()
        {
            try
            {
                Teams = _selectRepository.GetTeams().Select(t => t.TeamName).ToList();
                TeamComboBox.ItemsSource = Teams;

                Years = _selectRepository.GetSeasons().Select(s => s.Year).OrderByDescending(y => y).ToList();
                YearComboBox.ItemsSource = Years;

                TeamComboBox.SelectedIndex = -1;
                YearComboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams or years: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Load player and schedule data for the selected team and year
        private void LoadData(string teamName, int year)
        {
            try
            {
                _currentTeamName = teamName;
                _currentYear = year;

                // Load player details
                var team = _selectRepository.GetTeams(teamName: teamName).FirstOrDefault();
                if (team == null)
                {
                    ClearData();
                    return;
                }

                var season = _selectRepository.GetSeasons(year: year).FirstOrDefault();
                if (season == null)
                {
                    ClearData();
                    return;
                }

                PlayerDetails = _selectRepository.GetTeamPlayers(teamId: team.TeamId, seasonId: season.SeasonId)
                    .Select(tp =>
                    {
                        var player = _selectRepository.GetPlayers(playerId: tp.PlayerId).FirstOrDefault();
                        return player != null
                            ? new PlayerDetails
                            {
                                PlayerId = player.PlayerId,
                                PlayerName = player.PlayerName,
                                Position = player.Position,
                                JerseyNumber = tp.JerseyNumber
                            }
                            : null;
                    })
                    .Where(pd => pd != null)
                    .ToList();

                // Load game schedule
                ScheduleDetails = _repository.FetchGameSchedule(teamName, year).ToList();

                var conference = _selectRepository.GetConferences(confId: team.ConfId).FirstOrDefault();
                string conferenceName = conference?.ConfName ?? "Unknown Conference";
                TeamInfo = $"Team: {team.TeamName}, Location: {team.Location}, Mascot: {team.Mascot}, Conference: {conferenceName}";


                UpdateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Clear all data
        private void ClearData()
        {
            PlayerDetails.Clear();
            ScheduleDetails.Clear();
            TeamInfo = string.Empty;
            UpdateData();
        }

        // Update the UI with new data
        private void UpdateData()
        {
            // Update the data context, allowing blank lists to display nothing
            DataContext = null;
            DataContext = this;
        }

        private void OnTeamOrYearChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamComboBox.SelectedItem is string teamName && YearComboBox.SelectedItem is int year)
            {
                LoadData(teamName, year);
            }
            else
            {
                ClearData();
            }
        }

        private void BackToHomePage(object sender, RoutedEventArgs e)
        {
            CustomChange?.Invoke(this, new RoutedEventArgs());
        }

        private void NavigateToAddPlayer(object sender, RoutedEventArgs e)
        {
            AddPlayer?.Invoke(this, EventArgs.Empty);
        }

        private void NavigateToAddGame(object sender, RoutedEventArgs e)
        {
            AddGame?.Invoke(this, EventArgs.Empty);
        }

        private void NavigateToEditPlayer(object sender, RoutedEventArgs e)
        {
            EditPlayer?.Invoke(this, EventArgs.Empty);
        }

        private void NavigateToEditGame(object sender, RoutedEventArgs e)
        {
            EditGame?.Invoke(this, EventArgs.Empty);
        }

        private void NavigateToViewStats(object sender, RoutedEventArgs e)
        {
            ViewStats?.Invoke(this, EventArgs.Empty);
        }

        private void NavigateToEditStats(object sender, RoutedEventArgs e)
        {
            EditStats?.Invoke(this, EventArgs.Empty);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Refresh the dropdowns for team name and year
            LoadTeamsAndYears();

            // Reload the player and schedule data if team and year are selected
            if (!string.IsNullOrWhiteSpace(_currentTeamName) && _currentYear.HasValue)
            {
                LoadData(_currentTeamName, _currentYear.Value);
            }
        }
    }

    public class PlayerDetails
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Position { get; set; }
        public int JerseyNumber { get; set; }
    }
}