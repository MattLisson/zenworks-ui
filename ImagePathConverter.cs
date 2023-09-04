using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace Zenworks.UI {
    public class ImagePathConverter : IValueConverter {

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is string Name)) {
                return null;
            }

            string imagePath;
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                imagePath = Name;
            else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
                imagePath = Name + ".png";
            else if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
                imagePath = "Icons/" + Name + ".png";
            else
                throw new PlatformNotSupportedException("Unsupported platform for loading images");

            return FileImageSource.FromFile(imagePath);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
