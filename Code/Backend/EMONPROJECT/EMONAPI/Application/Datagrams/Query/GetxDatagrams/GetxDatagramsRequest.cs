using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Query.NewFolder
{
    public class GetxDatagramsRequest : IRequest<GetxDatagramsResponse>
    {
        public int amount;
        public GetxDatagramsRequest(int _amount)
        {
            amount = _amount;
        }
    }
}
