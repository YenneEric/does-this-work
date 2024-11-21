using System;
using System.Windows;
using System.Windows.Controls;
using PersonData;

namespace View
{
    public partial class AddOthers : UserControl
    {
        public event EventHandler<RoutedEventArgs>? CustomChange;
        private readonly IInsert _insertRepository;

        public AddOthers()
        {
            InitializeComponent();

            // Initialize the repository with the connection string
            const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";
            _insertRepository = new SqlInsertRepository(connectionString);
        }

        private void AddSeason_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(SeasonYearTextBox.Text, out int year))
            {
                try
                {
                    _insertRepository.CreateSeason(year);
                    MessageBox.Show($"Season {year} added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding season: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid year. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            string teamName = TeamNameTextBox.Text.Trim();
            string location = TeamLocationTextBox.Text.Trim();
            string mascot = TeamMascotTextBox.Text.Trim();
            string conferenceName = TeamConferenceTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(teamName) || string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(conferenceName))
            {
                MessageBox.Show("Please fill out all required fields (Team Name, Location, and Conference Name).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                int teamId = _insertRepository.CreateTeam(teamName, location, mascot, conferenceName);
                MessageBox.Show($"Team '{teamName}' added successfully with ID {teamId}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding team: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddConference_Click(object sender, RoutedEventArgs e)
        {
            string conferenceName = ConferenceNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(conferenceName))
            {
                MessageBox.Show("Please enter a valid Conference Name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _insertRepository.CreateConference(conferenceName);
                MessageBox.Show($"Conference '{conferenceName}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding conference: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToHomePage(object sender, RoutedEventArgs e)
        {
            CustomChange?.Invoke(this, new RoutedEventArgs());
        }
    }
}