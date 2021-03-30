using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Command.AddDatagram
{
    public class AddDatagramResponse
    {
        public string Id { get; set; }
        public AddDatagramResponse(string id)
        {
            Id = id;
        }
    }
}
