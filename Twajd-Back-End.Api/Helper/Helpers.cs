using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Api.Helper
{
    public class Helpers
    {
        public static TimeSpan strToTimeSpan(string time)
        {
            string[] formats = { "hhmm", "hmm", @"hh\:mm", @"h\:mm\:ss", @"h:mm", @"h:mm tt" };

            var result = DateTime.TryParseExact(time, formats, CultureInfo.CurrentCulture,
             DateTimeStyles.None, out var value);

            return value.TimeOfDay;
        }

        public static string TimeSpanToStr(TimeSpan timeSpan)
        {
            DateTime time = DateTime.Today.Add(timeSpan);
            string displayTime = time.ToString("hh:mm tt");
            return displayTime;
        }
    }
}
