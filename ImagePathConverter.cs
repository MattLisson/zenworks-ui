using System;
using System.Globalization;
using Xamarin.Forms;

namespace Zenworks.UI {
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
}
