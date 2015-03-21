using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServerTrack.Interfaces;
using ServerTrack.Models;

namespace ServerTrack.Controllers
{
    public class ServerLoadController : ApiController
    {
        private IServerDataRepository _serverDataRepository;

        public ServerLoadController(IServerDataRepository serverDataRepository)
        {
            this._serverDataRepository = serverDataRepository;
        }

        // GET api/serverload/server1
        public ServerLoadReport Get(string id)
        {
            return this._serverDataRepository.GetServerLoadStatistics(id);
        }

        // POST api/serverload
        public void Post([FromBody]LoadInfo load)
        {
            this._serverDataRepository.AddLoadInfo(load);
        }
    }
}
