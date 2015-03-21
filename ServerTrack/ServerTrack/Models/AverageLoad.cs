using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Models
{
    /// <summary>
    /// Average load for a time period 
    /// </summary>
    public class AverageLoad
    {
        public DateTime DateTime { get; set; }

        public double AverageCpuLoad { get; set; }

        public double AverageMemoryLoad { get; set; }

        public int NumberOfDataPoints { get; set; }

        public AverageLoad()
        {
        }

        public AverageLoad(DateTime dateTime)
        {
            this.DateTime = dateTime;
        }
    }
}