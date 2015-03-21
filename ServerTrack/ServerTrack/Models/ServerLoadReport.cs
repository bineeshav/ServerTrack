using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Models
{
    /// <summary>
    /// Average Load report for a server
    /// </summary>
    public class ServerLoadReport
    {
        public string ServerName { get; set; }

        public List<AverageLoad> AverageLoadByMinutes { get; set; }

        public List<AverageLoad> AverageLoadByHours { get; set; }

        public ServerLoadReport()
        {
            this.AverageLoadByMinutes = new List<AverageLoad>();
            this.AverageLoadByHours = new List<AverageLoad>();
        }
    }
}