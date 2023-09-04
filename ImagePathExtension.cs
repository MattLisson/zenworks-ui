using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;

namespace Zenworks.UI {
    [ContentProperty("SourceImage")]
    public class ImagePathExtension : IMarkupExtension<string?> {

        public string? Name { get; set; }

        public string? ProvideValue(IServiceProvider serviceProvider) {
            if (Name == null) {
                return null;
            }

            string imagePath = string.Empty;
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                imagePath = Name;
            else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
                imagePath = Name + ".png";
            else if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
                imagePath = "Icons/" + Name + ".png";
            else
                throw new PlatformNotSupportedException("Unsupported platform for loading images");

            return imagePath;
        }

        object? IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) {
            return ProvideValue(serviceProvider);
        }
    }
}
