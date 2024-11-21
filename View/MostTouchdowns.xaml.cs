using PersonData;
using PersonData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for MostTouchdowns.xaml
    /// </summary>
    public partial class MostTouchdowns : UserControl
    {
        public event EventHandler<RoutedEventArgs>? CustomChange;

        private readonly ISelect _repository;
        private readonly SqlTouchDownRepository _touchdownRepository;

        public MostTouchdowns()
        {
            InitializeComponent();
            const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";
            _repository = new SqlSelectRepository(connectionString);
            _touchdownRepository = new SqlTouchDownRepository(connectionString);

            InitializeComboBoxes();
        }

        private void FetchRankings_Click(object sender, RoutedEventArgs e)
        {
            // Get selected year and position from the combo boxes
            if (YearComboBox.SelectedItem is int selectedYear && PositionComboBox.SelectedItem is string selectedPosition)
            {
                try
                {
                    // Fetch ranking data
                    List<PlayerTouchdownRank> ranking = _touchdownRepository.FetchTouchdownsRank(selectedYear, selectedPosition);

                    // Bind the fetched data to the DataGrid
                    touchdownDataGrid.ItemsSource = ranking;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching rankings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select both a year and a position.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            LoadYears();
        }

        private void InitializeComboBoxes()
        {
            LoadYears();

            // Set hard-coded positions
            PositionComboBox.ItemsSource = new List<string> { "Quarterback", "Running Back", "Wide Receiver", "Tight End", "Defensive Back" };
            PositionComboBox.SelectedIndex = 0; // Set default selection
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

        private void BackToHomePage(object sender, RoutedEventArgs e)
        {
            CustomChange?.Invoke(this, new RoutedEventArgs());
        }
    }
}
