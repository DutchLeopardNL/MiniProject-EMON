using EMONAPI.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Query.GetLastTemprature
{
    public class GetLastTempratureResponse
    {
        public TempratureModel _temprature { get; }

        public GetLastTempratureResponse(TempratureModel temprature)
        {
            _temprature = temprature;
        }
    }
}
