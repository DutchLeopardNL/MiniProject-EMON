using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Command.AddDatagram
{
    public class AddDatagramCommand : IRequest<AddDatagramResponse>
    {

        public string Id { get; set; }

        public string timeStamp { get; set; }

        public double currentUsage { get; set; }

        public double totalLow { get; set; }
 
        public double totalHigh { get; set; }
    
        public double returnLow { get; set; }
 
        public double returnHigh { get; set; }

        public double gasUsage { get; set; }

        public string signature { get; set; }
    }
}
