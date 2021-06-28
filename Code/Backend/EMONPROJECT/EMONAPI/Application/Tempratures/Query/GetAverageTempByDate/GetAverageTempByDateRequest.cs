using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Query.GetAverageTempByDate
{
    public class GetAverageTempByDateRequest : IRequest<GetAverageTempByDateResponse>
    {
        public string _date { get; }
        public GetAverageTempByDateRequest(string date)
        {
            _date = date;
        }


    }
}
