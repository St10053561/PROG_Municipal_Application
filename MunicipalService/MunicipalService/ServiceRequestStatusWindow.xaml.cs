using MunicipalService.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MunicipalService
{
    /// <summary>
    /// Interaction logic for ServiceRequestWindow.xaml
    /// </summary>
    public partial class ServiceRequestWindow : Window
    {
        private List<IssueReport> previousReports; // List to store previous issue reports
        private List<IssueReport> originalReports; // List to store the original reports
        private BinarySearchTree bst; // Binary search tree for issue reports
        private MinHeap minHeap; // Min-heap for issue reports
        private ReportGraph reportGraph; // Graph structure for issue reports

        /// <summary>
        /// it initializes a new instance of the <see cref="ServiceRequestWindow"/> class.
        /// </summary>
        public ServiceRequestWindow()
        {
            InitializeComponent(); // Initialize the components
            LoadReports(); // Load reports from the temporary file

            // Initialize the ReportGraph
            reportGraph = new ReportGraph();

            // Add reports to the ReportGraph
            foreach (var report in previousReports)
            {
                reportGraph.AddReport(report.ReportNumber, report.Priority); // Add each report to the graph
            }

            // Initialize the timer for current time
            WeatherAndTimeUtility.InitializeTimer(CurrentTimeTextBlock); // Start the timer to update current time

            // Fetch and display current weather data
            WeatherAndTimeUtility.FetchWeatherData(CurrentTemperatureTextBlock); // Fetch and display weather data
        }

        /// <summary>
        /// It loads a Reports from the  file.
        /// </summary>
        private void LoadReports()
        {
            if (File.Exists(FormReportIssues.TempFilePath)) // Check if the temporary file exists
            {
                var json = File.ReadAllText(FormReportIssues.TempFilePath); // Read the JSON content from the file
                previousReports = JsonConvert.DeserializeObject<List<IssueReport>>(json) ?? new List<IssueReport>(); // Deserialize the JSON content to a list of IssueReport
                originalReports = new List<IssueReport>(previousReports); // Store the original reports

                // Initialize BST and MinHeap
                bst = new BinarySearchTree(); // Initialize the binary search tree
                minHeap = new MinHeap(); // Initialize the min-heap

                // Add reports to BST and MinHeap
                for (int i = 0; i < previousReports.Count; i++)
                {
                    previousReports[i].ReportNumber = i + 1; // Assign report numbers
                    bst.Insert(previousReports[i]); // Insert report into BST
                    minHeap.Insert(previousReports[i]); // Insert report into MinHeap
                }

                // Set the ItemsSource of the ReportsListBox
                ReportsListBox.ItemsSource = previousReports; // Bind the list of reports to the ListBox
                ReportsListBox.SelectionChanged += ReportsListBox_SelectionChanged; // Attach the selection changed event handler
            }
        }

        /// <summary>
        /// this is the event handler for the CloseDetailsButton Click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            ReportDetailsBorder.Visibility = Visibility.Collapsed; // Hide the report details
            ReportsListBox.SelectedItem = null; // Reset the selection to trigger the SelectionChanged event
        }

        /// <summary>
        /// This is the event handler for the ReportsListBox SelectionChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReportsListBox.SelectedItem != null) // Check if an item is selected
            {
                IssueReport selectedReport = (IssueReport)ReportsListBox.SelectedItem; // Get the selected report
                ReportTitleTextBlock.Text = selectedReport.Category.ToString(); // Display the report category
                ReportDateTextBlock.Text = selectedReport.Date.ToString("MM/dd/yyyy"); // Display the report date
                ReportStatusTextBlock.Text = selectedReport.Status; // Display the report status

                // Convert file paths to FileItem objects
                List<FileItem> fileItems = new List<FileItem>();
                foreach (var filePath in selectedReport.Attachments)
                {
                    fileItems.Add(new FileItem
                    {
                        FilePath = filePath, // Set the file path
                        FileName = System.IO.Path.GetFileName(filePath), // Set the file name
                        IsImage = filePath.EndsWith(".jpg") || filePath.EndsWith(".jpeg") || filePath.EndsWith(".png"), // Check if the file is an image
                    });
                }

                FilesListBox.ItemsSource = fileItems; // Bind the list of file items to the ListBox

                // Update the FileListBox to display the file icons
                foreach (var fileItem in fileItems)
                {
                    var listBoxItem = FilesListBox.ItemContainerGenerator.ContainerFromItem(fileItem) as ListBoxItem;
                    if (listBoxItem != null)
                    {
                        var image = new Image();
                        image.Source = new BitmapImage(new Uri(fileItem.FileIconPath, UriKind.Relative)); // Set the image source
                        listBoxItem.Content = image; // Set the content of the ListBoxItem to the image
                    }
                }

                ReportDetailsBorder.Visibility = Visibility.Visible; // Show the report details
            }
            else
            {
                ReportDetailsBorder.Visibility = Visibility.Collapsed; // Hide details if no selection
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimize the window
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Hide the window
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Enter Report Number") // Check if the default text is present
            {
                SearchTextBox.Text = ""; // Clear the text
                SearchTextBox.Foreground = new SolidColorBrush(Colors.Black); // Change the text color to black
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text)) // Check if the text is empty or whitespace
            {
                SearchTextBox.Text = "Enter Report Number"; // Reset the default text
                SearchTextBox.Foreground = new SolidColorBrush(Colors.Gray); // Change the text color to gray
            }
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if the left mouse button is pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Capture the mouse and move the window
                this.DragMove(); // Allow the window to be dragged
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int reportNumber;
            if (int.TryParse(SearchTextBox.Text, out reportNumber)) // Try to parse the report number
            {
                var report = bst.Search(reportNumber); // Search for the report in the BST
                if (report != null)
                {
                    ReportsListBox.SelectedItem = report; // Select the found report
                }
                else
                {
                    MessageBox.Show("Report not found."); // Show a message if the report is not found
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid report number."); // Show a message if the input is not valid
            }
        }

        private void ShowHighPriorityReportsButton_Click(object sender, RoutedEventArgs e)
        {
            // Filter high-priority reports
            var highPriorityReports = originalReports.Where(report => report.Priority == 1).ToList(); // Get high-priority reports

            // Set the ItemsSource of the ReportsListBox to the high-priority reports
            ReportsListBox.ItemsSource = highPriorityReports; // Bind the high-priority reports to the ListBox

            // Clear selection to avoid showing details
            ReportsListBox.SelectedItem = null; // Clear the selected report

            // Hide the report details
            ReportDetailsBorder.Visibility = Visibility.Collapsed; // Hide the report details

            // Notify the user if no high-priority reports are found
            if (highPriorityReports.Count == 0)
            {
                MessageBox.Show("No high-priority reports found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information); // Show a message if no high-priority reports are found
            }
        }

        private void ShowAllReportsButton_Click(object sender, RoutedEventArgs e)
        {
            ReportsListBox.ItemsSource = originalReports; // Reset to the original list of reports
            ReportsListBox.SelectedItem = null; // Clear the selected report
            ReportDetailsBorder.Visibility = Visibility.Collapsed; // Hide the report details
        }

        private void ShowResolutionTimes()
        {
            StringBuilder resolutionInfo = new StringBuilder(); // Create a StringBuilder to store resolution information

            foreach (var report in previousReports)
            {
                int resolutionTime = reportGraph.GetResolutionTime(report.ReportNumber); // Get the resolution time for each report
                resolutionInfo.AppendLine($"Report Number: {report.ReportNumber}, Title: {report.Title}, Resolution Time: {resolutionTime} minute(s)"); // Append the resolution information
            }

            MessageBox.Show(resolutionInfo.ToString(), "Resolution Times", MessageBoxButton.OK, MessageBoxImage.Information); // Show the resolution information
        }

        private void ShowResolutionTimesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowResolutionTimes(); // Show the resolution times when the button is clicked
        }
    }
}