using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Twajd_Back_End.Core.Helper
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

        public static double distance(string slat1, string slon1, string slat2, string slon2/*, char unit*/)
        {
            double lat1 = Convert.ToDouble(slat1);
            double lon1 = Convert.ToDouble(slon1);
            double lat2 = Convert.ToDouble(slat2);
            double lon2 = Convert.ToDouble(slon2);

            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                //if (unit == 'K')
                //{
                //    dist = dist * 1.609344;
                //}
                //else if (unit == 'N')
                //{
                //    dist = dist * 0.8684;
                //}
                dist = dist * 1.609344;
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

    }
}
