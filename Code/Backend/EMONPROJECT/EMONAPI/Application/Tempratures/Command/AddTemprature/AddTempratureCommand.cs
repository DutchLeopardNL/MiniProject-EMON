using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Command.AddTemprature
{
    public class AddTempratureCommand : IRequest<AddTempratureResponse>
    {

        public string Id { get; set; }

        public string timeStamp { get; set; }

        public string value { get; set; }
    }
}
