using ServerTrack.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Interfaces
{
    /// <summary>
    /// IServerDataRepository interface
    /// </summary>
    public interface IServerDataRepository
    {
        /// <summary>
        /// Adds the load information.
        /// </summary>
        /// <param name="loadInfo">The load information.</param>
        void AddLoadInfo(LoadInfo loadInfo);

        /// <summary>
        /// Gets the server load statistics.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <returns></returns>
        ServerLoadReport GetServerLoadStatistics(string serverName);
    }
}