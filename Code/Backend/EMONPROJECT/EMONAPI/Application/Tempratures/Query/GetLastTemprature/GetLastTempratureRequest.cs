using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Query.GetLastTemprature
{
    public class GetLastTempratureRequest : IRequest<GetLastTempratureResponse>
    {
        public GetLastTempratureRequest()
        {

        }
    }
}
