using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Helpers
{
    public class Utilities
    {
        /// <summary>
        /// Get the average.
        /// </summary>
        /// <param name="currentAverage">The current average.</param>
        /// <param name="numberOfCurrentDataPoints">The number of current data points.</param>
        /// <param name="newDataPoint">The new data point.</param>
        /// <returns></returns>
        public static double GetAverage(double currentAverage, double numberOfCurrentDataPoints, double newDataPoint)
        {
            double currentSum =  currentAverage * numberOfCurrentDataPoints;
            double average = (currentSum + newDataPoint)/(numberOfCurrentDataPoints + 1);
            return average;
        }
    }
}