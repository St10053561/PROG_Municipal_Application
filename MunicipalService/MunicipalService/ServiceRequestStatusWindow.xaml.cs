using MunicipalService.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MunicipalService
{
    /// <summary>
    /// Interaction logic for ServiceRequestWindow.xaml
    /// </summary>
    public partial class ServiceRequestWindow : Window
    {
        private List<IssueReport> previousReports;
        private List<IssueReport> originalReports; // Store the original reports
        private BinarySearchTree bst;
        private MinHeap minHeap;
        private ReportGraph reportGraph; // Add this line

        public ServiceRequestWindow()
        {
            InitializeComponent();
            LoadReports(); // Load reports from the temporary file

            // Initialize the ReportGraph
            reportGraph = new ReportGraph();

            // Add reports to the ReportGraph
            foreach (var report in previousReports)
            {
                reportGraph.AddReport(report.ReportNumber, report.Priority);
            }
        }

        private void LoadReports()
        {
            if (File.Exists(FormReportIssues.TempFilePath))
            {
                var json = File.ReadAllText(FormReportIssues.TempFilePath);
                previousReports = JsonConvert.DeserializeObject<List<IssueReport>>(json) ?? new List<IssueReport>();
                originalReports = new List<IssueReport>(previousReports); // Store the original reports

                // Initialize BST and MinHeap
                bst = new BinarySearchTree();
                minHeap = new MinHeap();

                // Add reports to BST and MinHeap
                for (int i = 0; i < previousReports.Count; i++)
                {
                    previousReports[i].ReportNumber = i + 1;
                    bst.Insert(previousReports[i]);
                    minHeap.Insert(previousReports[i]);
                }

                // Set the ItemsSource of the ReportsListBox
                ReportsListBox.ItemsSource = previousReports;
                ReportsListBox.SelectionChanged += ReportsListBox_SelectionChanged;
            }
        }

        private void CloseDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            ReportDetailsBorder.Visibility = Visibility.Collapsed; // Hide the report details
            ReportsListBox.SelectedItem = null; // Reset the selection to trigger the SelectionChanged event
        }

        private void ReportsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReportsListBox.SelectedItem != null)
            {
                IssueReport selectedReport = (IssueReport)ReportsListBox.SelectedItem;
                ReportTitleTextBlock.Text = selectedReport.Category.ToString();
                ReportDateTextBlock.Text = selectedReport.Date.ToString("MM/dd/yyyy");
                ReportStatusTextBlock.Text = selectedReport.Status;

                // Convert file paths to FileItem objects
                List<FileItem> fileItems = new List<FileItem>();
                foreach (var filePath in selectedReport.Attachments)
                {
                    fileItems.Add(new FileItem
                    {
                        FilePath = filePath,
                        FileName = System.IO.Path.GetFileName(filePath),
                        IsImage = filePath.EndsWith(".jpg") || filePath.EndsWith(".jpeg") || filePath.EndsWith(".png"),
                    });
                }

                FilesListBox.ItemsSource = fileItems;

                // Update the FileListBox to display the file icons
                foreach (var fileItem in fileItems)
                {
                    var listBoxItem = FilesListBox.ItemContainerGenerator.ContainerFromItem(fileItem) as ListBoxItem;
                    if (listBoxItem != null)
                    {
                        var image = new Image();
                        image.Source = new BitmapImage(new Uri(fileItem.FileIconPath, UriKind.Relative));
                        listBoxItem.Content = image;
                    }
                }

                ReportDetailsBorder.Visibility = Visibility.Visible;
            }
            else
            {
                ReportDetailsBorder.Visibility = Visibility.Collapsed; // Hide details if no selection
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Enter Report Number")
            {
                SearchTextBox.Text = "";
                SearchTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchTextBox.Text = "Enter Report Number";
                SearchTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int reportNumber;
            if (int.TryParse(SearchTextBox.Text, out reportNumber))
            {
                var report = bst.Search(reportNumber);
                if (report != null)
                {
                    ReportsListBox.SelectedItem = report;
                }
                else
                {
                    MessageBox.Show("Report not found.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid report number.");
            }
        }

        private void ShowHighPriorityReportsButton_Click(object sender, RoutedEventArgs e)
        {
            // Filter high-priority reports
            var highPriorityReports = originalReports.Where(report => report.Priority == 1).ToList();

            // Set the ItemsSource of the ReportsListBox to the high-priority reports
            ReportsListBox.ItemsSource = highPriorityReports;

            // Clear selection to avoid showing details
            ReportsListBox.SelectedItem = null;

            // Hide the report details
            ReportDetailsBorder.Visibility = Visibility.Collapsed;

            // Notify the user if no high-priority reports are found
            if (highPriorityReports.Count == 0)
            {
                MessageBox.Show("No high-priority reports found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ShowAllReports()
        {
            ReportsListBox.ItemsSource = originalReports; // Reset to the original list of reports

            // Automatically select the first item if there are any reports
            if (originalReports.Count > 0)
            {
                ReportsListBox.SelectedIndex = 0; // Select the first report
            }
        }

        private void ShowAllReportsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAllReports(); // Call the method to show all reports
        }

        private void ShowResolutionTimes()
        {
            StringBuilder resolutionInfo = new StringBuilder();

            foreach (var report in previousReports)
            {
                int resolutionTime = reportGraph.GetResolutionTime(report.ReportNumber);
                resolutionInfo.AppendLine($"Report Number: {report.ReportNumber}, Title: {report.Title}, Resolution Time: {resolutionTime} minute(s)");
            }

            MessageBox.Show(resolutionInfo.ToString(), "Resolution Times", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowResolutionTimesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowResolutionTimes();
        }
    }
}