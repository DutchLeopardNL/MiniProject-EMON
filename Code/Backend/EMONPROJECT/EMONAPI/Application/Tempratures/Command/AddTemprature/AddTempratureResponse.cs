using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Command.AddTemprature
{
    public class AddTempratureResponse
    {
        public string Id { get; set; }
        public AddTempratureResponse(string id)
        {
            Id = id;
        }
    }
}
