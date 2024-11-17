using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MunicipalService.Classes
{
    // This class converts a status string to a corresponding color for display purposes.
    public class StatusToColorConverter : IValueConverter
    {
        // This method converts the status value to a SolidColorBrush based on the status string.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                switch (status.ToLower())
                {
                    case "pending":
                        return new SolidColorBrush(Colors.Orange); // Orange for pending status
                    case "closed":
                        return new SolidColorBrush(Colors.Green); // Green for closed status
                    default:
                        return new SolidColorBrush(Colors.Gray); // Gray for any other status
                }
            }
            // If the value is not a string, return gray color
            return new SolidColorBrush(Colors.Gray);
        }

        // This method is not implemented because we don't need to convert back from color to status.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
