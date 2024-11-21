using System;
using System.Windows;
using System.Windows.Controls;
using PersonData;
using PersonData.Models;

namespace View
{
    public partial class EditStats : UserControl
    {
        private readonly IStatRepository _statRepository;
        private readonly ISelect _selectRepository;

        public event EventHandler? NavigateBack;

        public EditStats()
        {
            InitializeComponent();
            _statRepository = new SqlTouchDownRepository(@"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;");
            _selectRepository = new SqlSelectRepository(@"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;");
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (!int.TryParse(GameIdTextBox.Text, out var gameId))
                {
                    MessageBox.Show("Invalid Game ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var selectedTeam = TeamNameComboBox.SelectedItem as Team;
                if (selectedTeam == null || string.IsNullOrWhiteSpace(selectedTeam.TeamName))
                {
                    MessageBox.Show("Please select a valid Team Name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                if (!int.TryParse(PlayerIdTextBox.Text, out var playerId))
                {
                    MessageBox.Show("Invalid Player ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                int? rushingYards = ParseNullableInt(RushingYardsTextBox.Text);
                int? receivingYards = ParseNullableInt(ReceivingYardsTextBox.Text);
                int? throwingYards = ParseNullableInt(ThrowingYardsTextBox.Text);
                int? tackles = ParseNullableInt(TacklesTextBox.Text);
                int? sacks = ParseNullableInt(SacksTextBox.Text);
                int? turnovers = ParseNullableInt(TurnoversTextBox.Text);
                int? interceptionsCaught = ParseNullableInt(InterceptionsCaughtTextBox.Text);
                int? touchdowns = ParseNullableInt(TouchdownsTextBox.Text);
                int? punts = ParseNullableInt(PuntsTextBox.Text);
                int? fieldGoalsMade = ParseNullableInt(FieldGoalsMadeTextBox.Text);

                
                _statRepository.EditPlayerStats(
                    gameId,
                    selectedTeam.TeamName,
                    playerId,
                    rushingYards,
                    receivingYards,
                    throwingYards,
                    tackles,
                    sacks,
                    turnovers,
                    interceptionsCaught,
                    touchdowns,
                    punts,
                    fieldGoalsMade
                );

                MessageBox.Show("Player stats updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating player stats: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
