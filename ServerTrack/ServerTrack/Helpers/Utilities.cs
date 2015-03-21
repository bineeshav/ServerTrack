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
        public static int GetAverage(int currentAverage, int numberOfCurrentDataPoints, int newDataPoint)
        {
            decimal currentSum =  Convert.ToDecimal(currentAverage) * numberOfCurrentDataPoints;
            int average = Convert.ToInt32((currentSum + newDataPoint) / (numberOfCurrentDataPoints + 1));
            return average;
        }
    }
}