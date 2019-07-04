using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zenworks.UI {
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
}
