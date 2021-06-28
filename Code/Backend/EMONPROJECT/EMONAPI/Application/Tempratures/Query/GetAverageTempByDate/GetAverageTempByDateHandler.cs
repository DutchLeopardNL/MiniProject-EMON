using EMONAPI.Domain.Temprature;
using EMONAPI.Persistance.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Query.GetAverageTempByDate
{
    public class GetAverageTempByDateHandler : IRequestHandler<GetAverageTempByDateRequest, GetAverageTempByDateResponse>
    {
        private readonly ITempratureRepository _tempratureRepository;
        public GetAverageTempByDateHandler(ITempratureRepository tempratureRepository)
        {
            _tempratureRepository = tempratureRepository;
        }
        public async Task<GetAverageTempByDateResponse> Handle(GetAverageTempByDateRequest request, CancellationToken cancellationToken)
        {
            var tempratures = await _tempratureRepository.getTempratures(cancellationToken).ConfigureAwait(false);
            List<TempratureModel> tempraturesByDate = new List<TempratureModel>();
            foreach (var temprature in tempratures)
            {
                if (temprature.timeStamp.Contains(request._date))
                {
                    tempraturesByDate.Add(temprature);
                }
            }
            double averageTemp = 0;
            foreach (var temp in tempraturesByDate)
            {
                averageTemp += Convert.ToDouble(temp.value)/100;
            }
            averageTemp = averageTemp / tempraturesByDate.Count;
            averageTemp = Math.Round(averageTemp, 2);
            if(tempraturesByDate.Count == 0)
            {
                averageTemp = 0;
            }
            return new GetAverageTempByDateResponse(averageTemp);
        }
    }
    }
