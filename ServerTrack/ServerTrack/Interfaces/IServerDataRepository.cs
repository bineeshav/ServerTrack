using ServerTrack.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Interfaces
{
    public interface IServerDataRepository
    {
        void AddLoadInfo(LoadInfo loadInfo);

        ServerLoadReport GetServerLoadStatistics(string serverName);
    }
}