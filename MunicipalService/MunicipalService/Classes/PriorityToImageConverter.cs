using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MunicipalService.Classes
{
    public class PriorityToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int priority)
            {
                // Return the corresponding image path based on priority
                if (priority == 1) // Assuming 1 is for high priority
                {
                    return "Images/high_priority_icon.png";
                }
            }
            return null; // Return null if no priority or not high priority
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
