using EMONAPI.Domain.Datagram;
using EMONAPI.Persistance.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Query.GetLastDatagram
{
    public class GetLastDatagramHandler : IRequestHandler<GetLastDatagramRequest,GetLastDatagramResponse>
    {
        private readonly IDatagramRepository _datagramRepository;
        public GetLastDatagramHandler(IDatagramRepository datagramRepository)
        {
            _datagramRepository = datagramRepository;
        }

        public async Task<GetLastDatagramResponse> Handle(GetLastDatagramRequest request, CancellationToken cancellationToken)
        {
            var datagrams = await _datagramRepository.GetDatagrams(cancellationToken).ConfigureAwait(false);


            List<FullDatagram> sortedDatagrams = datagrams.OrderBy(datagram => datagram.timeStamp).Reverse().ToList();

            return new GetLastDatagramResponse(sortedDatagrams[0]);
        }
    }
}
