using MunicipalService.Classes;
using System;
using System.Collections.Generic;
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
        private BinarySearchTree bst;
        private MinHeap minHeap;

        public ServiceRequestWindow(List<IssueReport> previousReports)
        {
            InitializeComponent();
            this.previousReports = previousReports;

            bst = new BinarySearchTree();
            minHeap = new MinHeap();

            for (int i = 0; i < previousReports.Count; i++)
            {
                previousReports[i].ReportNumber = i + 1;
                bst.Insert(previousReports[i]);
                minHeap.Insert(previousReports[i]);
            }

            ReportsListBox.ItemsSource = previousReports;
            ReportsListBox.SelectionChanged += ReportsListBox_SelectionChanged;
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
                ReportDetailsBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
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
            var highPriorityReports = minHeap.GetAllReports();
            ReportsListBox.ItemsSource = highPriorityReports;
        }
    }
}
