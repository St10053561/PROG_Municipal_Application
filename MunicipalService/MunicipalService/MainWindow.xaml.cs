using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using MunicipalService.Classes;
using Newtonsoft.Json.Linq;

namespace MunicipalService
{
    public partial class MainWindow : Window
    {
        // Constructor for MainWindow
        public MainWindow()
        {
            InitializeComponent(); // Initialize the components of the window
            WeatherAndTimeUtility.InitializeTimer(CurrentTimeTextBlock); // Start the timer to update current time
            WeatherAndTimeUtility.FetchWeatherData(CurrentTemperatureTextBlock); // Fetch and display current weather data
            DisableButtons(); // Disable certain buttons initially
        }

        // Event handler for Minimize button click
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimize the window
        }

        // Event handler for Close button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Close the application
        }

        // Event handler for Report button click
        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            FormReportIssues reportIssuesForm = new FormReportIssues(); // Create a new form for reporting issues
            reportIssuesForm.Show(); // Show the report issues form
            this.Hide(); // Hide the main window
        }

        // Event handler for Feedback button click
        private void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackSection.Visibility = Visibility.Visible; // Show the feedback section
        }

        // Event handler for Submit Feedback button click
        private void SubmitFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the feedback text from the rich text box
            string feedback = new TextRange(FeedbackRichTxtbox.Document.ContentStart, FeedbackRichTxtbox.Document.ContentEnd).Text;
            // Show a message box thanking the user for their feedback
            MessageBox.Show("Thank you for your feedback!", "Feedback Submitted", MessageBoxButton.OK, MessageBoxImage.Information);
            FeedbackRichTxtbox.Document.Blocks.Clear(); // Clear the feedback text box
            FeedbackSection.Visibility = Visibility.Collapsed; // Hide the feedback section
        }

        // Event handler for Cancel Feedback button click
        private void CancelFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackSection.Visibility = Visibility.Collapsed; // Hide the feedback section
        }

        // Method to disable certain buttons initially
        private void DisableButtons()
        {
            LocalBtn.IsEnabled = true; // Enable the Local button
            ServiceBtn.IsEnabled = true; // Enable the Service button
            ServiceBtn.Click += ServiceBtn_Click; // Attach event handler for Service button click
        }

        // Event handler for Service button click
        private void ServiceBtn_Click(object sender, RoutedEventArgs e)
        {// Get the reports from ReportStorage
            List<IssueReport> issueReports = ReportStorage.GetReports();

            // Create a new window to display the reports
            //ServiceRequestWindow viewReportsWindow = new ServiceRequestWindow(issueReports);
            ServiceRequestWindow viewReportsWindow = new ServiceRequestWindow(); 
            viewReportsWindow.ShowDialog(); // Show the reports window as a dialog
        }

        // Event handler for View Reports button click
        private void ViewReportsBtn_Click(object sender, RoutedEventArgs e)
        {
            //// Get the reports from ReportStorage
            //List<IssueReport> issueReports = ReportStorage.GetReports();

            //// Create a new window to display the reports
            DisplayReport displayReport = new DisplayReport();
            displayReport.Show(); // Show the reports window
            this.Hide(); // Hide the main window
        }

        // Event handler for Show Events button click
        private void ShowEventsButton_Click(object sender, RoutedEventArgs e)
        {
            //EventsPopup.IsOpen = true; // Open the events popup
            MessageBox.Show("This feature is currently under construction. Please check back later for updates.", "Under Construction", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Event handler for Close Popup button click
        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            EventsPopup.IsOpen = false; // Close the events popup
            Application.Current.Shutdown(); // Close the application
        }


        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if the left mouse button is pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Capture the mouse and move the window
                this.DragMove();
            }
        }

        // Event handler for Local button click
        private void LocalBtn_Click(object sender, RoutedEventArgs e)
        {
            LocalEventsWindow localEventsWindow = new LocalEventsWindow(this); // Create a new window for local events
            localEventsWindow.Show(); // Show the local events window
            this.Hide(); // Hide the main window
        }
    }
}
