using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Query.GetLastDatagram
{
    public class GetLastDatagramRequest : IRequest<GetLastDatagramResponse>
    {
        public GetLastDatagramRequest()
        {

        }
    }
}
