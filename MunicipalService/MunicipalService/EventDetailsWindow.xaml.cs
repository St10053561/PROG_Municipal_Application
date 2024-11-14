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
using System.Windows.Threading;

namespace MunicipalService
{
    /// <summary>
    /// Interaction logic for EventDetailsWindow.xaml
    /// </summary>
    public partial class EventDetailsWindow : Window
    {
        // Timer to count down to the event date
        private DispatcherTimer countdownTimer;
        // Date and time of the event
        private DateTime eventDate;

        // Constructor for EventDetailsWindow
        public EventDetailsWindow(Events selectedEvent)
        {
            InitializeComponent(); // Initialize the components of the window
            DataContext = selectedEvent; // Bind the event details to the window's DataContext

            // Set the window title to the event name
            this.Title = $"Event - {selectedEvent.EventName}";

            eventDate = selectedEvent.EventDate; // Set the event date
            InitializeCountdownTimer(); // Initialize the countdown timer
        }

        // Method to initialize the countdown timer
        private void InitializeCountdownTimer()
        {
            countdownTimer = new DispatcherTimer(); // Create a new DispatcherTimer
            countdownTimer.Interval = TimeSpan.FromSeconds(1); // Set the timer interval to 1 second
            countdownTimer.Tick += CountdownTimer_Tick; // Attach the Tick event handler
            countdownTimer.Start(); // Start the timer
        }

        // Event handler for the countdown timer Tick event
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            // Calculate the time remaining until the event
            TimeSpan timeRemaining = eventDate - DateTime.Now;
            if (timeRemaining.TotalSeconds > 0)
            {
                // Update the countdown text block with the time remaining
                CountdownTextBlock.Text = $"Time until event: {timeRemaining.Days}d {timeRemaining.Hours}h {timeRemaining.Minutes}m {timeRemaining.Seconds}s";
            }
            else
            {
                // Update the countdown text block to indicate the event is happening now
                CountdownTextBlock.Text = "The event is happening now!";
                countdownTimer.Stop(); // Stop the timer
            }
        }

        // Event handler for the Close button click event
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the window
        }
    }
}
