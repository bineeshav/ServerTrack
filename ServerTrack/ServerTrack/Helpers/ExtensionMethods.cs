using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Tops the of the minute.
        /// </summary>
        /// <param name="d">The datetime.</param>
        /// <returns>datetime ignoring seconds</returns>
        public static DateTime TopOfTheMinute(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour,d.Minute, 0);
        }
        
        /// <summary>
        /// Tops the of the hour.
        /// </summary>
        /// <param name="d">The datetime.</param>
        /// <returns>datetime ignoring minute and seconds</returns>
        public static DateTime TopOfTheHour(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, 0, 0);
        }
    }
}