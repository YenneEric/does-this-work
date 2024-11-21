using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PersonData;
using PersonData.Models;

namespace View
{
    /// <summary>
    /// Interaction logic for MostTeamYards.xaml
    /// </summary>
    public partial class MostTeamYards : UserControl
    {
        public event EventHandler<RoutedEventArgs>? CustomChange;

        private readonly ISelect _repository;
        private readonly SqlMostTeamYards _teamYardsRepository;

        public MostTeamYards()
        {
            InitializeComponent();

            // Initialize repositories
            const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";
            _repository = new SqlSelectRepository(connectionString);
            _teamYardsRepository = new SqlMostTeamYards(connectionString);

            // Load ComboBox data
            LoadYears();
        }

        private void LoadYears()
        {
            try
            {
                // Fetch available years dynamically from the database
                var seasons = _repository.GetSeasons();
                YearComboBox.ItemsSource = seasons.Select(season => season.Year).ToList();
                YearComboBox.SelectedIndex = 0; // Default selection
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading years: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FetchTeamYards_Click(object sender, RoutedEventArgs e)
        {
            // Get selected year from combo box
            if (YearComboBox.SelectedItem is int selectedYear)
            {
                try
                {
                    // Fetch team yards data
                    IReadOnlyList<PersonData.Models.MostTeamYards> teamYardsList = _teamYardsRepository.FetchMostTeamYards(selectedYear);

                    // Bind the fetched data to the DataGrid
                    teamYardsDataGrid.ItemsSource = teamYardsList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching team yards: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid year.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BackToHomePage(object sender, RoutedEventArgs e)
        {
            CustomChange?.Invoke(this, new RoutedEventArgs());
        }
    }
}