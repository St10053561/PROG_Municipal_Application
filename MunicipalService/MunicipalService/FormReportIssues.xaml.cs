using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using MunicipalService.Classes;
using Newtonsoft.Json;

namespace MunicipalService
{
    /// <summary>
    /// Interaction logic for FormReportIssues.xaml
    /// </summary>
    public partial class FormReportIssues : Window
    {
        public const string TempFilePath = "tempReports.json";
        List<IssueReport> issueReports = new List<IssueReport>(); // List to store issue reports
        private bool isImageUploaded = false; // Flag to check if an image is uploaded
        private List<string> attachedFiles = new List<string>(); // List to store attached files
        private BackgroundWorker backgroundWorker; // BackgroundWorker for handling background tasks
        private MainWindow mainWindow;
        public ObservableCollection<FileItem> Files { get; set; } = new ObservableCollection<FileItem>();
        ReportGraph reportGraph = new ReportGraph();

        private int GetNextReportNumber()
        {
            return issueReports.Count + 1;
        }

        public FormReportIssues()
        {
            InitializeComponent(); // Initialize the components
            this.Closed += FormReportIssues_FormClosed; // Event handler for form closed event
            this.progressBar.Visibility = Visibility.Hidden; // Hide the progress bar initially

            // Load reports from temporary file
            LoadReportsFromTempFile();

            // Remove expired reports after loading
            RemoveExpiredReports();

            // Initialize BackgroundWorker
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true; // Enable progress reporting
            backgroundWorker.DoWork += BackgroundWorker_DoWork; // Event handler for DoWork event
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged; // Event handler for ProgressChanged event

            FilesItemsControl.ItemsSource = Files; // Bind the ItemsControl to the Files collection
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Exception Handling for the form fields
                ExceptionHandling.ValidateFormFields(LocationTxtbx.Text, CategoryComboBx.Text, new TextRange(DescriptionRichTxtbox.Document.ContentStart, DescriptionRichTxtbox.Document.ContentEnd).Text);

                // Create new report
                IssueReport newReport = new IssueReport
                {
                    ReportNumber = GetNextReportNumber(), // Set the report number
                    Title = LocationTxtbx.Text, // Set the title or name
                    Location = LocationTxtbx.Text, // Set the location
                    Category = (CategoryComboBx.SelectedItem as ComboBoxItem).Content.ToString(), // Set the category
                    Description = new TextRange(DescriptionRichTxtbox.Document.ContentStart, DescriptionRichTxtbox.Document.ContentEnd).Text, // Set the description
                    Attachments = new List<string>(attachedFiles), // Set the attachments
                    Date = DateTime.Now, // Set the date to the current date
                    ImagePaths = attachedFiles.Where(IsImageFile).ToList(), // Set the image paths if any images are uploaded
                    Priority = PriorityCheckBox.IsChecked == true ? 1 : 0 // Set priority based on checkbox
                };

                // Add the new report to the ReportStorage
                ReportStorage.AddReport(newReport);
                issueReports.Add(newReport); // Add the new report to the local list

                // Add the new report to the ReportGraph
                reportGraph.AddReport(newReport.ReportNumber, newReport.Priority); // Add report to graph

                // Save reports to temporary file
                SaveReportsToTempFile();

                // After validating and storing the issue
                MessageBox.Show("Thank you for reporting! Your issue has been submitted.", "Submission Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear the form fields
                LocationTxtbx.Clear();
                CategoryComboBx.SelectedIndex = -1;
                DescriptionRichTxtbox.Document.Blocks.Clear();
                progressBar.Value = 0;
                attachedFiles.Clear();
                isImageUploaded = false;
                Files.Clear();
                PriorityCheckBox.IsChecked = false; // Reset the checkbox
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Information); // Show validation error message
            }
        }

        private void BackToMainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            // Check if any field has a value
            bool isFormFilled = !string.IsNullOrWhiteSpace(LocationTxtbx.Text) ||
                                CategoryComboBx.SelectedIndex != -1 ||
                                !string.IsNullOrWhiteSpace(new TextRange(DescriptionRichTxtbox.Document.ContentStart, DescriptionRichTxtbox.Document.ContentEnd).Text) ||
                                attachedFiles.Count > 0;

            if (isFormFilled)
            {
                // Show confirmation dialog
                MessageBoxResult result = MessageBox.Show("You have unsaved changes. Are you sure you want to go back to the main menu?", "Unsaved Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // Navigate back to Main Menu
                    MainWindow mainMenu = new MainWindow();
                    mainMenu.Show();
                    this.Close(); // Close the current window
                }
            }
            else
            {
                // Navigate back to Main Menu
                MainWindow mainMenu = new MainWindow();
                mainMenu.Show();
                this.Close(); // Close the current window
            }
        }

        private void MediaAttachBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Create an OpenFileDialog instance
            openFileDialog.Filter = "Image and Document Files(*.jpg; *.jpeg; *.gif; *.bmp; *.pdf; *.doc; *.docx; *.txt)|*.jpg; *.jpeg; *.gif; *.bmp; *.pdf; *.doc; *.docx; *.txt"; // Set the file filter
            openFileDialog.Multiselect = true; // Allow multiple file selection

            if (openFileDialog.ShowDialog() == true)
            {
                attachedFiles.AddRange(openFileDialog.FileNames); // Add selected files to the attached files list

                foreach (var filePath in openFileDialog.FileNames)
                {
                    AddFile(filePath, System.IO.Path.GetFileName(filePath), IsImageFile(filePath));
                }

                // Reset the progress bar for file upload
                progressBar.Value = 0;
                progressBar.Visibility = Visibility.Visible; // Show the progress bar
                backgroundWorker.RunWorkerAsync(); // Start the background worker

                // Set the image upload flag
                isImageUploaded = true;

                // Disable the Submit button until the upload is complete
                SubmitBtn.IsEnabled = false;
            }
        }

        private async void LocationTxtbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Only fetch suggestions if the input is not empty
            if (!string.IsNullOrWhiteSpace(LocationTxtbx.Text))
            {
                var locationService = new LocationService();
                var locationSuggestions = await locationService.GetLocationSuggestionsAsync(LocationTxtbx.Text);

                // Display the location suggestions in a dropdown list or autocomplete box
                LocationListBox.ItemsSource = locationSuggestions;

                // Show the Popup if there are suggestions
                LocationPopup.IsOpen = locationSuggestions.Any();
            }
            else
            {
                // Close the Popup if the input is empty
                LocationPopup.IsOpen = false;
            }
        }

        private void LocationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if an item is selected
            if (LocationListBox.SelectedItem != null)
            {
                // Set the TextBox text to the selected item from the ListBox
                LocationTxtbx.Text = (string)LocationListBox.SelectedItem;

                // Close the Popup
                LocationPopup.IsOpen = false;

                // Optionally, you could clear the selection to allow re-selection
                LocationListBox.SelectedItem = null;
            }
        }

        // Helper method to check if the file is an image
        private bool IsImageFile(string filePath)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            return imageExtensions.Contains(System.IO.Path.GetExtension(filePath).ToLower());
        }

        private void AddFile(string filePath, string fileName, bool isImage)
        {
            Files.Add(new FileItem { FilePath = filePath, FileName = fileName, IsImage = isImage });
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                // Simulate work
                System.Threading.Thread.Sleep(20); // Sleep for 20 milliseconds
                backgroundWorker.ReportProgress(i); // Report progress
            }
        }

        private List<IssueReport> GetPreviousReports()
        {
            // For demonstration purposes, return the existing issueReports list
            return issueReports;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage; // Update the progress bar value

            // Enable the Submit button when progress reaches 100%
            if (e.ProgressPercentage == 100)
            {
                SubmitBtn.IsEnabled = true;
            }
        }

        /// <summary>
        /// It allows to close the Form safely and exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormReportIssues_FormClosed(object sender, EventArgs e)
        {
            //Application.Current.Shutdown(); // Close the entire application
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimize the window
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if any field has a value
            bool isFormFilled = !string.IsNullOrWhiteSpace(LocationTxtbx.Text) ||
                                CategoryComboBx.SelectedIndex != -1 ||
                                !string.IsNullOrWhiteSpace(new TextRange(DescriptionRichTxtbox.Document.ContentStart, DescriptionRichTxtbox.Document.ContentEnd).Text) ||
                                attachedFiles.Count > 0;

            if (isFormFilled)
            {
                MessageBoxResult result = MessageBox.Show("You have unsaved changes. If you close this form, your changes will not be saved. Are you sure you want to close this form?", "Unsaved Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.MainWindow.Show();
                    this.Hide();
                }
            }
            else
            {
                Application.Current.MainWindow.Show();
                this.Hide();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is FileItem fileItem)
            {
                Files.Remove(fileItem); // Remove the file item from the collection
                attachedFiles.Remove(fileItem.FilePath); // Remove the file path from the attached files list
            }
        }

        private void SaveReportsToTempFile()
        {
            var json = JsonConvert.SerializeObject(issueReports);
            File.WriteAllText(TempFilePath, json);
        }

        private void LoadReportsFromTempFile()
        {
            if (File.Exists(TempFilePath))
            {
                var json = File.ReadAllText(TempFilePath);
                issueReports = JsonConvert.DeserializeObject<List<IssueReport>>(json) ?? new List<IssueReport>();
            }
        }

        private void RemoveExpiredReports()
        {
            if (File.Exists(TempFilePath))
            {
                var json = File.ReadAllText(TempFilePath);
                var reports = JsonConvert.DeserializeObject<List<IssueReport>>(json) ?? new List<IssueReport>();

                // Remove reports older than 7 days
                reports.RemoveAll(report => (DateTime.Now - report.Date).TotalDays > 7);

                // Save the remaining reports back to the file
                var updatedJson = JsonConvert.SerializeObject(reports);
                File.WriteAllText(TempFilePath, updatedJson);

                // Update the local issueReports list
                issueReports = reports;
            }
        }
    }
}
