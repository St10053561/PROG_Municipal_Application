using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MunicipalService.Classes
{
    // This class converts a priority value to an image path for display purposes.
    public class PriorityToImageConverter : IValueConverter
    {
        // This method converts the priority value to the corresponding image path.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int priority)
            {
                // If the priority is 1 (assuming 1 is high priority), return the high priority icon path.
                if (priority == 1)
                {
                    return "Images/high_priority_icon.png";
                }
            }
            // If the priority is not 1 or the value is not an integer, return null.
            return null;
        }

        // This method is not implemented because we don't need to convert back from image to priority.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
