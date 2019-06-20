using System;
using System.Globalization;
using NodaTime;
using NodaTime.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zenworks.UI {
    public static class UIUtils {

        // TODO: Translate these nice human readable patterns to other locales
        // or otherwise use standard patterns.
        private static readonly DurationPattern hms = DurationPattern.CreateWithCurrentCulture("H'h' m'm' s's'");
        public static string FormatHMS(this Duration duration) {
            return hms.Format(duration);
        }

        private static readonly InstantPattern shortPattern = InstantPattern.CreateWithCurrentCulture("g");
        public static string Format(this Instant instant) {
            return shortPattern.Format(instant);
        }
    }

    [ContentProperty("SourceImage")]
    public class ImagePathExtension : IMarkupExtension<string?> {

        public string? Name { get; set; }

        public string? ProvideValue(IServiceProvider serviceProvider) {
            if (Name == null) {
                return null;
            }

            string imagePath;
            switch (Device.RuntimePlatform) {
                case Device.Android:
                    imagePath = Name;
                    break;
                case Device.iOS:
                    imagePath = Name + ".png";
                    break;
                case Device.UWP:
                    imagePath = "Icons/" + Name + ".png";
                    break;
                default:
                    throw new PlatformNotSupportedException("Unsupported platform for loading images");
            }
            return imagePath;
        }

        object? IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) {
            return ProvideValue(serviceProvider);
        }
    }

    public class AnimPathExtension : IMarkupExtension<string?> {

        public string? Name { get; set; }

        public string? ProvideValue(IServiceProvider serviceProvider) {
            if (Name == null) {
                return null;
            }

            string imagePath;
            switch (Device.RuntimePlatform) {
                case Device.Android:
                    imagePath = Name + ".json";
                    break;
                case Device.iOS:
                    imagePath = Name + ".json";
                    break;
                case Device.UWP:
                    imagePath = "Animations/" + Name + ".json";
                    break;
                default:
                    throw new PlatformNotSupportedException("Unsupported platform for loading Animations");
            }
            return imagePath;
        }

        object? IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) {
            return ProvideValue(serviceProvider);
        }
    }

    public class ImagePathConverter : IValueConverter {

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is string Name)) {
                return null;
            }

            string imagePath;
            switch (Device.RuntimePlatform) {
                case Device.Android:
                    imagePath = Name;
                    break;
                case Device.iOS:
                    imagePath = Name + ".png";
                    break;
                case Device.UWP:
                    imagePath = "Icons/" + Name + ".png";
                    break;
                default:
                    throw new PlatformNotSupportedException("Unsupported platform for loading images");
            }
            return FileImageSource.FromFile(imagePath);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    // This doesnt work for some reason (Not called even when referenced as a static resource).
    public class BooleanNegationConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(bool)value;
        }
    }

    public class IntEnumConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Enum) {
                return (int)value;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is int) {
                return Enum.ToObject(targetType, value);
            }
            return 0;
        }
    }

    public class DurationStringConverter : IValueConverter {
        private static readonly DurationPattern hm = DurationPattern.CreateWithCurrentCulture("H'h' m'm'");
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is double) {
                value = Duration.FromMinutes((double)value);
            }
            if (value is Duration) {
                return hm.Format((Duration)value);
            } else {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is string)) {
                return value;
            }
            ParseResult<Duration> result = hm.Parse(value as string);
            if (!result.Success) {
                return value;
            }
            return result.Value;
        }
    }
}
