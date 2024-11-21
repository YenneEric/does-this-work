using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PersonData;
using PersonData.Models;

namespace View
{
    
    public partial class ConfrenceWins : UserControl
    {
        public event EventHandler<RoutedEventArgs>? CustomChange;

        private readonly ISelect _repository;

        public ConfrenceWins()
        {
            InitializeComponent();

            
            const string connectionString = @"Server=(localdb)\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;";
            _repository = new SqlSelectRepository(connectionString);

            
            LoadYears();
            LoadConferences();
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

        private void LoadConferences()
        {
            try
            {
                
                var conferences = _repository.GetConferences();
                ConferenceComboBox.ItemsSource = conferences.Select(conf => conf.ConfName).ToList();
                ConferenceComboBox.SelectedIndex = 0; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading conferences: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FetchConferenceTeamRanks_Click(object sender, RoutedEventArgs e)
        {
            if (YearComboBox.SelectedItem is int selectedYear && ConferenceComboBox.SelectedItem is string selectedConference)
            {
                try
                {
                    var conferenceTeamRanks = new SqlConferenceWinsRepository("Server=(localdb)\\MSSQLLocalDb;Database=tuesday;Integrated Security=SSPI;")
                        .FetchConferenceTeamRank(selectedYear, selectedConference);

                    conferenceRankDataGrid.ItemsSource = conferenceTeamRanks;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching rankings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select both a year and a conference.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            LoadYears();
            LoadConferences();

        }

        private void BackToHomePage(object sender, RoutedEventArgs e)
        {
            CustomChange?.Invoke(this, new RoutedEventArgs());
        }
    }
}