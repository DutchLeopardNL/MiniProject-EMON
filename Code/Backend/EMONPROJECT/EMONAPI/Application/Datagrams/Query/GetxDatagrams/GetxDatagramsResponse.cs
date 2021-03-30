using EMONAPI.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Query.NewFolder
{
    public class GetxDatagramsResponse
    {
        public List<FullDatagram> _datagrams { get; }
        public GetxDatagramsResponse(List<FullDatagram> datagrams)
        {
            _datagrams = datagrams;
        }
    }
}
