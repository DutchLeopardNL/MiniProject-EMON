using EMONAPI.Domain.Datagram;
using EMONAPI.Domain.Temprature;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Query.GetxTempratures
{
    public class GetxTempraturesHandler: IRequestHandler<GetxTempraturesRequest,GetxTempraturesResponse>
    {
        private readonly ITempratureRepository _tempratureRepository;

        public GetxTempraturesHandler(ITempratureRepository tempratureRepository)
        {
            _tempratureRepository = tempratureRepository;
        }
        public async Task<GetxTempraturesResponse> Handle(GetxTempraturesRequest request, CancellationToken cancellationToken)
        {
            var tempratures = await _tempratureRepository.getTempratures(cancellationToken).ConfigureAwait(false);
            var lastx = tempratures.OrderBy(temprature => temprature.timeStamp.ToUpper()).Reverse().Take(request.amount);
            return new GetxTempraturesResponse(lastx.ToList());
        }
    }
}
