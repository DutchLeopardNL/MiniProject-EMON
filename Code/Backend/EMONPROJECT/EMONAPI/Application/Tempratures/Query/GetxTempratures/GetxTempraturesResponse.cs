using EMONAPI.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Query.GetxTempratures
{
    public class GetxTempraturesResponse
    {
        public List<TempratureModel> _tempratures { get; }
        public GetxTempraturesResponse(List<TempratureModel> tempratures)
        {
            _tempratures = tempratures;
        }
    }
}
