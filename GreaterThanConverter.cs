using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Zenworks.UI {
    public class GreaterThanConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is not string stringParameter)
                return false;
            
            if (value is int number) {
                int threshold = int.Parse(stringParameter);
                return number > threshold;
            } else if (value is double dNumber) {
                double threshold = double.Parse(stringParameter);
                return dNumber > threshold;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException("Can't convert greater than bool to int.");
        }
    }
}
