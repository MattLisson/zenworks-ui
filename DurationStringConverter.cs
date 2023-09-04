using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using NodaTime;
using NodaTime.Text;

namespace Zenworks.UI {
    public class DurationStringConverter : IValueConverter {
        private static readonly DurationPattern hm = DurationPattern.CreateWithCurrentCulture("H'h' m'm'");
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is double) {
                value = Duration.FromMinutes((double)value);
            }
            if (value is Duration) {
                return hm.Format((Duration)value);
            } else {
                return value.ToString()!;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is string valueString)) {
                return value;
            }
            ParseResult<Duration> result = hm.Parse(valueString);
            if (!result.Success) {
                return value;
            }
            return result.Value;
        }
    }

    public class DurationToWholeSeconds : IValueConverter {
        private static readonly DurationPattern hm = DurationPattern.CreateWithCurrentCulture("S's'");
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Duration) {
                return hm.Format((Duration)value);
            } else {
                return value.ToString()!;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is string valueString)) {
                return value;
            }
            ParseResult<Duration> result = hm.Parse(valueString);
            if (!result.Success) {
                return value;
            }
            return result.Value;
        }
    }
}
