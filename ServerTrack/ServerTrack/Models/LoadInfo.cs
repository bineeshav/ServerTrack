using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServerTrack.Interfaces;

namespace ServerTrack.Models
{
    /// <summary>
    /// Load information for a server for the given time
    /// </summary>
    public class LoadInfo
    {
        public string ServerName { get; set; }

        public DateTime DateTime { get; set; }

        public double CpuLoad { get; set; }

        public double MemoryLoad { get; set; }

        public LoadInfo(string serverName, DateTime dateTime, int cpuLoad, int memoryLoad)
        {
            this.ServerName = serverName;
            this.DateTime = dateTime;
            this.CpuLoad = cpuLoad;
            this.MemoryLoad = memoryLoad;
        }
    }
}