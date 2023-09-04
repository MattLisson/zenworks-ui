using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Zenworks.UI {
    // This doesnt work for some reason (Not called even when referenced as a static resource).
    public class BooleanNegationConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(bool)value;
        }
    }
}
