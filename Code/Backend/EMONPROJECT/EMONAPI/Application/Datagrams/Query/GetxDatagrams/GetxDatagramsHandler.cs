using EMONAPI.Domain.Datagram;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Query.NewFolder
{
    public class GetxDatagramsHandler: IRequestHandler<GetxDatagramsRequest,GetxDatagramsResponse>
    {
        private readonly IDatagramRepository _datagramRepository;

        public GetxDatagramsHandler(IDatagramRepository datagramRepository)
        {
            _datagramRepository = datagramRepository;
        }
        public async Task<GetxDatagramsResponse> Handle(GetxDatagramsRequest request, CancellationToken cancellationToken)
        {
            var datagrams = await _datagramRepository.GetDatagrams(cancellationToken).ConfigureAwait(false);
            var lastx = datagrams.OrderBy(datagram => datagram.timeStamp).Reverse().Take(request.amount);
            return new GetxDatagramsResponse(lastx.ToList());
        }
    }
}
