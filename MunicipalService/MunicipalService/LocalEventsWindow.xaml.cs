using MunicipalService.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MunicipalService
{
    /// <summary>
    /// Interaction logic for LocalEventsWindow.xaml
    /// </summary>
    public partial class LocalEventsWindow : Window
    {
        // Dictionary to store events sorted by date
        private SortedDictionary<DateTime, Events> eventsDictionary;
        // Set to store unique event categories
        private HashSet<string> categoriesSet;
        // Queue to store upcoming events
        private Queue<Events> upcomingEventsQueue;
        // Stack to store search history
        private Stack<string> searchHistory;
        // Dictionary to store search frequency
        private Dictionary<string, int> searchFrequency;
        // Reference to the main window
        private MainWindow mainWindow;
        // Dictionary to store open event details windows
        private Dictionary<Events, EventDetailsWindow> openEventDetailsWindows = new Dictionary<Events, EventDetailsWindow>();

        // Constructor for LocalEventsWindow
        public LocalEventsWindow(MainWindow mainWindow)
        {
            InitializeComponent(); // Initialize the components of the window
            this.mainWindow = mainWindow; // Set the reference to the main window
            WeatherAndTimeUtility.InitializeTimer(CurrentTimeTextBlock); // Start the timer to update current time
            WeatherAndTimeUtility.FetchWeatherData(CurrentTemperatureTextBlock); // Fetch and display current weather data
            InitializeEventData(); // Initialize event data
        }

        /// <summary>
        /// Initializes event data and sets up initial display
        /// </summary>
        private void InitializeEventData()
        {
            eventsDictionary = new SortedDictionary<DateTime, Events>(); // Initialize the events dictionary
            categoriesSet = new HashSet<string> { "Government", "Festival", "Community", "Workshop", "Sports" }; // Initialize the categories set
            upcomingEventsQueue = new Queue<Events>(); // Initialize the upcoming events queue
            searchHistory = new Stack<string>(); // Initialize the search history stack
            searchFrequency = new Dictionary<string, int>(); // Initialize the search frequency dictionary

            // Load sample events
            var sampleEvents = Events.GetSampleEvents();
            foreach (var ev in sampleEvents)
            {
                eventsDictionary[ev.EventDate] = ev; // Add event to the dictionary
                upcomingEventsQueue.Enqueue(ev); // Add event to the queue
            }

            // Display all events initially
            EventsList.ItemsSource = eventsDictionary.Values.ToList();
        }

        /// <summary>
        /// Handles the search button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchText = SearchTextBox.Text; // Get the search text
            var selectedDate = EventDatePicker.SelectedDate; // Get the selected date

            // Save search history
            searchHistory.Push(searchText);

            // Update search frequency
            if (!string.IsNullOrEmpty(searchText))
            {
                if (searchFrequency.ContainsKey(searchText))
                {
                    searchFrequency[searchText]++;
                }
                else
                {
                    searchFrequency[searchText] = 1;
                }
            }

            var filteredEvents = eventsDictionary.Values.AsEnumerable();

            // Filter events by search text
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredEvents = filteredEvents.Where(ev =>
                    ev.EventCategory.Equals(searchText, StringComparison.OrdinalIgnoreCase) ||
                    ev.EventName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Filter events by selected date range
            if (selectedDate.HasValue)
            {
                var startDate = selectedDate.Value.AddDays(-3);
                var endDate = selectedDate.Value.AddDays(3);

                filteredEvents = filteredEvents.Where(ev => ev.EventDate.Date >= startDate.Date && ev.EventDate.Date <= endDate.Date);
            }

            var filteredEventsList = filteredEvents.ToList();

            // Show message if no events found
            if (filteredEventsList.Count == 0)
            {
                MessageBox.Show("No events found matching the search criteria.", "Search Results", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Update the events list with filtered events
            EventsList.ItemsSource = filteredEventsList;

            // Recommend events based on search category
            if (!string.IsNullOrEmpty(searchText))
            {
                var category = eventsDictionary.Values.FirstOrDefault(ev => ev.EventName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)?.EventCategory;
                if (!string.IsNullOrEmpty(category))
                {
                    var recommendedEvents = Events.GetRecommendedEventsByCategory(category);
                    RecommendedEventsList.ItemsSource = recommendedEvents;
                }
                else
                {
                    RecommendedEventsList.ItemsSource = null;  // No recommendations found
                }
            }
            else
            {
                RecommendedEventsList.ItemsSource = null;  // No search text
            }
        }

        /// <summary>
        /// Gets the category of the search text if it matches any predefined categories
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private string GetSearchCategory(string searchText)
        {
            foreach (var category in categoriesSet)
            {
                if (searchText.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    return category;
                }
            }

            return null;  // No matching category found
        }

        /// <summary>
        /// Minimizes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimize the window
        }

        /// <summary>
        /// Closes the window and shows the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show(); // Show the main window
            this.Close(); // Close the current window
        }

        /// <summary>
        /// Handles the event selection change in the events list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Events selectedEvent = EventsList.SelectedItem as Events; // Get the selected event
            if (selectedEvent != null)
            {
                EventDetailsWindow eventDetailsWindow = new EventDetailsWindow(selectedEvent); // Create a new event details window
                eventDetailsWindow.Show(); // Show the event details window

                // Display recommended events based on the selected event category
                var recommendedEvents = Events.GetRecommendedEventsByCategory(selectedEvent.EventCategory);
                RecommendedEventsList.ItemsSource = recommendedEvents;
            }
        }

        /// <summary>
        /// Recommends related events based on the most frequent search term
        /// </summary>
        private void RecommendRelatedEvents()
        {
            if (searchFrequency.Count > 0)
            {
                var mostSearchedTerm = searchFrequency.OrderByDescending(kvp => kvp.Value).First().Key;

                var recommendedEvents = eventsDictionary.Values
                    .Where(ev => ev.EventCategory.Equals(mostSearchedTerm, StringComparison.OrdinalIgnoreCase) ||
                                 ev.EventName.IndexOf(mostSearchedTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (recommendedEvents.Count > 0)
                {
                    RecommendedEventsList.ItemsSource = recommendedEvents;
                }
                else
                {
                    RecommendedEventsList.ItemsSource = null;  // No recommendations found
                }
            }
            else
            {
                RecommendedEventsList.ItemsSource = null;  // No search history available for recommendations
            }
        }

        /// <summary>
        /// Handles the event selection change in the recommended events list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecommendedEventsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Events selectedRecommendedEvent = RecommendedEventsList.SelectedItem as Events; // Get the selected recommended event
            if (selectedRecommendedEvent != null)
            {
                EventDetailsWindow eventDetailsWindow = new EventDetailsWindow(selectedRecommendedEvent); // Create a new event details window
                eventDetailsWindow.ShowDialog(); // Show the event details window as a dialog
            }
        }

        /// <summary>
        /// Handles the category selection change in the category combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem != null)
            {
                var selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem).Content.ToString(); // Get the selected category
                System.Diagnostics.Debug.WriteLine($"Selected category: {selectedCategory}");

                var filteredEvents = eventsDictionary.Values.Where(ev => ev.EventCategory.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)).ToList();
                System.Diagnostics.Debug.WriteLine($"Filtered events count: {filteredEvents.Count}");

                EventsList.ItemsSource = filteredEvents; // Update the events list with filtered events

                // Recommend events based on selected category
                var recommendedEvents = Events.GetRecommendedEventsByCategory(selectedCategory);
                System.Diagnostics.Debug.WriteLine($"Recommended events count: {recommendedEvents.Count}");
                RecommendedEventsList.ItemsSource = recommendedEvents;
            }
            else
            {
                EventsList.ItemsSource = eventsDictionary.Values.ToList(); // Display all events if no category is selected
                RecommendedEventsList.ItemsSource = null; // Clear recommended events list
            }
        }
    }
}
