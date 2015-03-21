using System;
using System.Collections.Generic;
using System.Linq;
using ServerTrack.Interfaces;

namespace ServerTrack.Models
{
    /// <summary>
    ///     Repository for server load infomation
    /// </summary>
    public class ServerDataMemoryRepository : IServerDataRepository
    {
        /// <summary>
        ///     The server data dictionary
        /// </summary>
        private static Dictionary<string, ServerLoadData> _serverDataDictionary;

        public ServerDataMemoryRepository()
        {
            _serverDataDictionary = new Dictionary<string, ServerLoadData>();
        }

        /// <summary>
        ///     Adds the load.
        /// </summary>
        /// <param name="loadInfo">The load information.</param>
        public void AddLoadInfo(LoadInfo loadInfo)
        {
            if (! _serverDataDictionary.ContainsKey(loadInfo.ServerName))
            {
                //this is a new server
                InitializeServerLoad(loadInfo);
            }

            ServerLoadData loadData = _serverDataDictionary[loadInfo.ServerName];
            loadData.AddLoadInfo(loadInfo);
        }

        /// <summary>
        ///     Gets the server load averages.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns></returns>
        public ServerLoadReport GetServerLoadStatistics(string serverName)
        {
            var report = new ServerLoadReport();
            report.ServerName = serverName;
            if (_serverDataDictionary.ContainsKey(serverName))
            {
                IEnumerable<KeyValuePair<DateTime, AverageLoad>> minuteAverages =
                    _serverDataDictionary[serverName].AverageByMinutes.Where(x => x.Key > DateTime.Now.AddMinutes(-60));
                foreach (var item in minuteAverages)
                {
                    report.AverageLoadByMinutes.Add(item.Value);
                }


                IEnumerable<KeyValuePair<DateTime, AverageLoad>> hourlyAverages =
                    _serverDataDictionary[serverName].AverageByMinutes.Where(x => x.Key > DateTime.Now.AddHours(-24));
                foreach (var item in minuteAverages.GroupBy(x => x.Key.Hour))
                {
                    int cpuLoadAverage = Convert.ToInt32((item.Average(x => x.Value.AverageCpuLoad)));
                    int memoryLoadAverage = Convert.ToInt32(item.Average(x => x.Value.AverageMemoryLoad));
                    int numberOfDataPoints = Convert.ToInt32(item.Sum(x => x.Value.NumberOfDataPoints));
                    report.AverageLoadByHours.Add(new AverageLoad(item.Min(x => x.Key))
                    {
                        AverageCpuLoad = cpuLoadAverage,
                        AverageMemoryLoad = memoryLoadAverage
                    });
                }
            }
            return report;
        }

        /// <summary>
        ///     Initializes the server.
        /// </summary>
        /// <param name="loadInfo">The load information.</param>
        private void InitializeServerLoad(LoadInfo loadInfo)
        {
            lock (_serverDataDictionary)
            {
                if (! _serverDataDictionary.ContainsKey(loadInfo.ServerName))
                {
                    _serverDataDictionary.Add(loadInfo.ServerName, new ServerLoadData(loadInfo.ServerName));
                }
            }
        }

        public void PurgeData()
        {
            throw new NotImplementedException();
        }
    }
}