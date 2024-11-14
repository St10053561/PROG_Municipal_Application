using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MunicipalService.Classes
{
    // Helper class to add placeholder text functionality to TextBox controls
    public class TextBoxHelper
    {
        // DependencyProperty to hold the placeholder text
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderText", // Name of the property
                typeof(string), // Type of the property
                typeof(TextBoxHelper), // Owner type
                new PropertyMetadata(string.Empty, OnPlaceholderTextChanged) // Default value and property changed callback
            );

        // Getter method for the PlaceholderText property
        public static string GetPlaceholderText(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderTextProperty);
        }

        // Setter method for the PlaceholderText property
        public static void SetPlaceholderText(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderTextProperty, value);
        }

        // Callback method when the PlaceholderText property changes
        private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox) // Check if the DependencyObject is a TextBox
            {
                // Attach event handlers for GotFocus and LostFocus events
                textBox.GotFocus += RemovePlaceholder;
                textBox.LostFocus += ShowPlaceholder;

                // Show the placeholder if the TextBox is not focused
                if (!textBox.IsFocused)
                {
                    ShowPlaceholder(textBox, null);
                }
            }
        }

        // Event handler to remove the placeholder text when the TextBox gains focus
        private static void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == GetPlaceholderText(textBox)) // Check if the TextBox text matches the placeholder
            {
                textBox.Text = string.Empty; // Clear the TextBox text
                textBox.Foreground = Brushes.Black; // Set the text color to black
            }
        }

        // Event handler to show the placeholder text when the TextBox loses focus
        private static void ShowPlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrEmpty(textBox.Text)) // Check if the TextBox is empty
            {
                textBox.Text = GetPlaceholderText(textBox); // Set the TextBox text to the placeholder
                textBox.Foreground = Brushes.Gray; // Set the text color to gray
            }
        }
    }
}
