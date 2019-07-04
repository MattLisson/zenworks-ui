using NodaTime;
using NodaTime.Text;

namespace Zenworks.UI {
    public static class UIUtils {

        // TODO: Translate these nice human readable patterns to other locales
        // or otherwise use standard patterns.
        private static readonly DurationPattern hms = DurationPattern.CreateWithCurrentCulture("H'h' mm'm' ss's'");
        public static string FormatHMS(this Duration duration) {
            return hms.Format(duration);
        }

        private static readonly InstantPattern shortPattern = InstantPattern.CreateWithCurrentCulture("g");
        public static string Format(this Instant instant) {
            return shortPattern.Format(instant);
        }
    }
}
