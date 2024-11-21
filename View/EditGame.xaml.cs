using System;
using System.Windows;
using System.Windows.Controls;
using PersonData;
using PersonData.Models;

namespace View
{
    
    public partial class EditGame : UserControl
    {
        private readonly IUpdate _updateRepository;
        private readonly ISelect _selectRepository;

        public event EventHandler? NavigateBack;

        const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";

        public EditGame()
        {
            InitializeComponent();
            _updateRepository = new SqlUpdateRepository(connectionString);
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

        
        public void LoadGameDetails(
            int gameId,
            DateTime gameDate,
            string gameLocation,
            string homeTeam,
            int? homeScore,
            int? homeTop,
            string awayTeam,
            int? awayScore,
            int? awayTop)
        {
            GameIdTextBox.Text = gameId.ToString();
            GameDatePicker.SelectedDate = gameDate;
            GameLocationTextBox.Text = gameLocation;

            Team1ComboBox.SelectedItem = homeTeam;
            Team1ScoreTextBox.Text = homeScore?.ToString();
            Team1TopTextBox.Text = homeTop?.ToString();

            Team2ComboBox.SelectedItem = awayTeam;
            Team2ScoreTextBox.Text = awayScore?.ToString();
            Team2TopTextBox.Text = awayTop?.ToString();
        }

        
        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!int.TryParse(GameIdTextBox.Text, out var gameId))
                {
                    MessageBox.Show("Invalid Game ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var gameDate = GameDatePicker.SelectedDate;
                var gameLocation = string.IsNullOrWhiteSpace(GameLocationTextBox.Text) ? null : GameLocationTextBox.Text;

                var homeTeam = Team1ComboBox.SelectedItem as Team;
                var awayTeam = Team2ComboBox.SelectedItem as Team;

                int? homeTop = ParseNullableInt(Team1TopTextBox.Text);
                int? homeScore = ParseNullableInt(Team1ScoreTextBox.Text);

                int? awayTop = ParseNullableInt(Team2TopTextBox.Text);
                int? awayScore = ParseNullableInt(Team2ScoreTextBox.Text);

                
                _updateRepository.UpdateGameDetails(
                    gameId,
                    homeTeam?.TeamName,
                    homeScore,
                    homeTop,
                    awayTeam?.TeamName,
                    awayScore,
                    awayTop,
                    gameLocation,
                    gameDate
                );

                MessageBox.Show("Game details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                
                NavigateBack?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving game details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
