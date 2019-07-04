using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zenworks.UI {
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
}
