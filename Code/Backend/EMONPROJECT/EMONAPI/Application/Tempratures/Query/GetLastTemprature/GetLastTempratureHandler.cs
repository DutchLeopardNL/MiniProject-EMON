using EMONAPI.Domain.Datagram;
using EMONAPI.Domain.Temprature;
using EMONAPI.Persistance.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Query.GetLastTemprature
{
    public class GetLastTempratureHandler : IRequestHandler<GetLastTempratureRequest,GetLastTempratureResponse>
    {
        private readonly ITempratureRepository _tempratureRepository;
        public GetLastTempratureHandler(ITempratureRepository tempratureRepository)
        {
            _tempratureRepository = tempratureRepository;
        }

        public async Task<GetLastTempratureResponse> Handle(GetLastTempratureRequest request, CancellationToken cancellationToken)
        {
            var tempratures = await _tempratureRepository.getTempratures(cancellationToken).ConfigureAwait(false);
            List<TempratureModel> sortedTempratures = tempratures.OrderBy(temprature => temprature.timeStamp.ToUpper()).Reverse().ToList();
            return new GetLastTempratureResponse(sortedTempratures[0]);
        }
    }
}
