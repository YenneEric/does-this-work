using System;
using System.Windows;
using System.Windows.Controls;
using PersonData; 
using PersonData.Models; 

namespace View
{
    public partial class AddGame : UserControl
    {
        private readonly IInsert _insertRepository;
        private readonly ISelect _selectRepository;

        public event EventHandler? NavigateBack;

        const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";

        public AddGame()
        {
            InitializeComponent();
            _insertRepository = new SqlInsertRepository(connectionString);
            _selectRepository = new SqlSelectRepository(connectionString);
            LoadTeams();
        }

        private void LoadTeams()
        {
            try
            {
                var teams = _selectRepository.GetTeams();
                Team1ComboBox.ItemsSource = teams;
                Team2ComboBox.ItemsSource = teams;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var gameDate = GameDatePicker.SelectedDate;
                var location = GameLocationTextBox.Text;
                var team1 = Team1ComboBox.SelectedItem as Team;
                var team2 = Team2ComboBox.SelectedItem as Team;

                int? team1Top = ParseNullableInt(Team1TopTextBox.Text);
                int? team1Score = ParseNullableInt(Team1ScoreTextBox.Text);
                int? team2Top = ParseNullableInt(Team2TopTextBox.Text);
                int? team2Score = ParseNullableInt(Team2ScoreTextBox.Text);

                if (gameDate == null || string.IsNullOrWhiteSpace(location) || team1 == null || team2 == null)
                {
                    MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                if (team1.TeamId == team2.TeamId)
                {
                    MessageBox.Show("Home and Away teams cannot be the same.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var gameId = _insertRepository.CreateGame((DateTime)gameDate, location);

                
                _insertRepository.CreateGameTeam(gameId, team1.TeamName, teamTypeId: 1, topOfPossessionSec: team1Top, score: team1Score); 
                _insertRepository.CreateGameTeam(gameId, team2.TeamName, teamTypeId: 2, topOfPossessionSec: team2Top, score: team2Score); 

                MessageBox.Show("Game and teams added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                
                NavigateBack?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding game: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int? ParseNullableInt(string input)
        {
            return int.TryParse(input, out var value) ? value : (int?)null;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack?.Invoke(this, EventArgs.Empty);
        }
    }
}
