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

        private static readonly DurationPattern S = DurationPattern.CreateWithCurrentCulture("%S");
        public static string FormatS(this Duration duration) {
            return S.Format(duration);
        }

        private static readonly ZonedDateTimePattern shortPattern = ZonedDateTimePattern.CreateWithCurrentCulture("MMM dd h:mm tt", DateTimeZoneProviders.Tzdb);
        public static string Format(this Instant instant) {
            return shortPattern.Format(instant.InZone(currentTimezone));
        }

        private static readonly DateTimeZone currentTimezone = DateTimeZoneProviders.Tzdb.GetSystemDefault();

        private static readonly ZonedDateTimePattern hm = ZonedDateTimePattern.CreateWithCurrentCulture("h:mm tt", DateTimeZoneProviders.Tzdb);
        public static string FormatShort(this Instant instant) {
            return hm.Format(instant.InZone(currentTimezone));
        }
    }
}
