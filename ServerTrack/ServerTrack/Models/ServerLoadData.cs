using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using ServerTrack.Helpers;
using ServerTrack.Interfaces;

namespace ServerTrack.Models
{
    /// <summary>
    /// Aggregated load information for a server
    /// </summary>
    public class ServerLoadData
    {
        private string _serverName;

        /// <summary>
        /// The average load by minutes
        /// </summary>
        private Dictionary<DateTime, AverageLoad> _AverageLoadByMinutes;

        public ServerLoadData(string name)
        {
            this._serverName = name;
            this._AverageLoadByMinutes = new Dictionary<DateTime, AverageLoad>();
        }

        /// <summary>
        /// Gets the average by minutes.
        /// </summary>
        /// <value>
        /// The average by minutes.
        /// </value>
        public Dictionary<DateTime, AverageLoad> AverageByMinutes
        {
            get
            {
                return _AverageLoadByMinutes;
                
            }
        }

        /// <summary>
        /// Adds the load information.
        /// </summary>
        /// <param name="loadInfo">The load information.</param>
        public void AddLoadInfo(LoadInfo loadInfo)
        {
            DateTime minute = loadInfo.DateTime.TopOfTheMinute();

            if(this._AverageLoadByMinutes.ContainsKey(minute) == false)
            {
                InitializeMinute(minute);
            }

            AverageLoad averageLoad = this._AverageLoadByMinutes[minute] as AverageLoad;
            averageLoad.AverageCpuLoad = Utilities.GetAverage(averageLoad.AverageCpuLoad, averageLoad.NumberOfDataPoints, loadInfo.CpuLoad);
            averageLoad.AverageMemoryLoad = Utilities.GetAverage(averageLoad.AverageMemoryLoad, averageLoad.NumberOfDataPoints, loadInfo.MemoryLoad);
            ++averageLoad.NumberOfDataPoints;
        }

        /// <summary>
        /// Initializes the minute.
        /// </summary>
        /// <param name="minute">The minute.</param>
        private void InitializeMinute(DateTime minute)
        {
            lock (this._AverageLoadByMinutes)
            {
                if (!this._AverageLoadByMinutes.ContainsKey(minute))
                {
                    AverageLoad averageLoad = new AverageLoad(minute);

                    this._AverageLoadByMinutes.Add(minute, averageLoad);
                }

                //TODO : need not call purge every minute.
                Purge();
            }
        }

        /// <summary>
        /// Purges this instance.
        /// </summary>
        private void Purge()
        {
            //remove all those are older than x days
            //hard coded x as 10 for now
            var itemsToRemove = this._AverageLoadByMinutes.Where(x => x.Key < DateTime.Now.AddHours((-24 * 10)));
            foreach (var item in itemsToRemove)
            {
                this._AverageLoadByMinutes.Remove(item.Key);
            }

        }
    }
} 