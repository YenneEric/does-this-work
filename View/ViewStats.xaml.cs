using System;
using System.Windows;
using System.Windows.Controls;
using PersonData;
using PersonData.Models;

namespace View
{
    public partial class ViewStats : UserControl
    {
        private readonly IStatRepository _statRepository;
        private readonly ISelect _selectRepository;

        public event EventHandler? NavigateBack;

        const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";

        public ViewStats()
        {
            InitializeComponent();
            _statRepository = new SqlTouchDownRepository(connectionString);
            _selectRepository = new SqlSelectRepository(connectionString);
            LoadTeams();
        }

        private void LoadTeams()
        {
            try
            {
                var teams = _selectRepository.GetTeams();
                TeamNameComboBox.ItemsSource = teams;
                TeamNameComboBox.DisplayMemberPath = "TeamName"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!int.TryParse(GameIdTextBox.Text, out var gameId))
                {
                    MessageBox.Show("Please enter a valid Game ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var selectedTeam = TeamNameComboBox.SelectedItem as Team;
                if (selectedTeam == null || string.IsNullOrWhiteSpace(selectedTeam.TeamName))
                {
                    MessageBox.Show("Please select a valid Team Name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int? playerId = null;
                if (!string.IsNullOrWhiteSpace(PlayerIdTextBox.Text))
                {
                    if (int.TryParse(PlayerIdTextBox.Text, out var parsedPlayerId))
                    {
                        playerId = parsedPlayerId;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Player ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                
                var playerStats = _statRepository.FetchPlayerStatsByGameAndTeam(gameId, selectedTeam.TeamName, playerId);

                
                ResultsListView.ItemsSource = playerStats;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching player stats: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack?.Invoke(this, EventArgs.Empty);
        }
    }
}
