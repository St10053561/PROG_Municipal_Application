using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MunicipalService.Classes
{
    // This class helps to convert a boolean value to a Visibility value and vice versa.
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        // This method converts a boolean value to a Visibility value.
        // If the boolean is true, it returns Collapsed. If false, it returns Visible.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible;
        }

        // This method converts a Visibility value back to a boolean.
        // If the Visibility is not Visible, it returns true. Otherwise, it returns false.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility != Visibility.Visible;
            }
            return true;
        }
    }
}
