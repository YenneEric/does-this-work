using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PersonData; 
using PersonData.Models; 

namespace View
{
    public partial class AddPlayerPage : UserControl
    {
        public event EventHandler? NavigateBack;


        private readonly ISelect _repository; 
        private readonly IInsert _insertRepository; 

        public AddPlayerPage()
        {
            InitializeComponent();

            
            const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";
            _repository = new SqlSelectRepository(connectionString);
            _insertRepository = new SqlInsertRepository(connectionString);

            LoadPositions();
            LoadTeams();
            LoadYears();
        }

        private void LoadPositions()
        {
            PositionComboBox.ItemsSource = new List<string>
            {
                "Quarterback", "Running Back", "Wide Receiver", "Tight End",
                "Linebacker", "Cornerback", "Safety", "Kicker", "Punter"
            };
        }

        private void LoadTeams()
        {
            try
            {
                var teams = _repository.GetTeams();
                TeamComboBox.ItemsSource = teams.Select(team => team.TeamName).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadYears()
        {
            try
            {
                var seasons = _repository.GetSeasons();
                YearComboBox.ItemsSource = seasons.Select(season => season.Year.ToString()).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading years: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var playerName = PlayerNameTextBox.Text;
                var position = PositionComboBox.SelectedItem?.ToString();
                var teamName = TeamComboBox.SelectedItem?.ToString();
                var year = YearComboBox.SelectedItem?.ToString();
                var jerseyNumberText = JerseyNumberTextBox.Text;

                if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(position) ||
                    string.IsNullOrWhiteSpace(teamName) || string.IsNullOrWhiteSpace(year) ||
                    string.IsNullOrWhiteSpace(jerseyNumberText))
                {
                    MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(jerseyNumberText, out var jerseyNumber) || jerseyNumber <= 0)
                {
                    MessageBox.Show("Jersey number must be a positive integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var playerId = _insertRepository.CreatePlayer(playerName, position);

                
                _insertRepository.CreateTeamPlayer(jerseyNumber, year, teamName, playerId);

                MessageBox.Show($"Player '{playerName}' added as '{position}' to team '{teamName}' for year '{year}' with jersey number '{jerseyNumber}'",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                NavigateBack?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding player: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToInsertData(object sender, RoutedEventArgs e)
        {
            NavigateBack?.Invoke(this, EventArgs.Empty);
        }
    }
}
