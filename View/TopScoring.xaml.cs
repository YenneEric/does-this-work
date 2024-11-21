using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PersonData;
using PersonData.Models;

namespace View
{
    
    public partial class TopScoring : UserControl
    {
        public event EventHandler<RoutedEventArgs>? CustomChange;

        private readonly ISelect _repository;
        private readonly IStatRepository _topScoringRepository;

        public TopScoring()
        {
            InitializeComponent();

            const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";
            _repository = new SqlSelectRepository(connectionString);
            _topScoringRepository = new SqlTouchDownRepository(connectionString);

            LoadYears();
        }

        private void LoadYears()
        {
            try
            {
                var seasons = _repository.GetSeasons();
                YearComboBox.ItemsSource = seasons.Select(season => season.Year).ToList();
                YearComboBox.SelectedIndex = 0; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading years: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FetchTopScoringTeams_Click(object sender, RoutedEventArgs e)
        {
            
            if (YearComboBox.SelectedItem is int selectedYear)
            {
                try
                {
                    
                    List<TopScoringTeamRank> topScoringTeams = _topScoringRepository.FetchTopScoringTeams(selectedYear);

                    
                    topScoringTeamsDataGrid.ItemsSource = topScoringTeams;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching top-scoring teams: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid year.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            LoadYears();

        }

        private void BackToHomePage(object sender, RoutedEventArgs e)
        {
            CustomChange?.Invoke(this, new RoutedEventArgs());
        }
    }
}