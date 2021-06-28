using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Query.GetAverageTempByDate
{
    public class GetAverageTempByDateResponse
    {
        public double _averageTemp { get;}

        public GetAverageTempByDateResponse(double averageTemp)
        {
            _averageTemp = averageTemp;
        }
    }
}
