using MunicipalService.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MunicipalService
{
    /// <summary>
    /// Interaction logic for ViewReportsWindow.xaml
    /// </summary>
    public partial class ServiceRequestWindow : Window
    {
        private List<IssueReport> previousReports;
        public ServiceRequestWindow(List<IssueReport> previousReports)
        {
            InitializeComponent();
            this.previousReports = previousReports;
            for (int i = 0; i < previousReports.Count; i++)
            {
                previousReports[i].ReportNumber = i + 1;
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
                foreach (var filePath in selectedReport.Attachments) // Use Attachments instead of ImagePaths
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
    }
}
