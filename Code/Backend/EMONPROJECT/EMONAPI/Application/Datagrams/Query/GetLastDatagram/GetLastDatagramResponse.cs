using EMONAPI.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Query.GetLastDatagram
{
    public class GetLastDatagramResponse
    {
        public FullDatagram _datagram { get; }

        public GetLastDatagramResponse(FullDatagram datagram)
        {
            _datagram = datagram;
        }
    }
}
