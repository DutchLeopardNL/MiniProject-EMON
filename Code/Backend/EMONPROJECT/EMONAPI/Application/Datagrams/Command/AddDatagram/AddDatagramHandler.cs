using EMONAPI.Domain.Datagram;
using EMONAPI.Persistance.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Application.Datagrams.Command.AddDatagram
{
    public class AddDatagramHandler : IRequestHandler<AddDatagramCommand,AddDatagramResponse>
    {
        private readonly IDatagramRepository _datagramRepo;
        public AddDatagramHandler(IDatagramRepository datagramRepository)
        {
            _datagramRepo = datagramRepository;
        }
        public async Task<AddDatagramResponse> Handle(AddDatagramCommand request, CancellationToken cancellation)
        {
            var id = Guid.NewGuid().ToString();
            FullDatagram datagram = new FullDatagram
            {
                Id = id,
                timeStamp = DateTime.Now.ToString("dd_MMM_yyyy_HH_mm_ss"),
                currentUsage = request.currentUsage,
                totalLow = request.totalLow,
                totalHigh = request.totalHigh,
                returnLow = request.returnLow,
                returnHigh = request.returnHigh,
                gasUsage = request.gasUsage,
                signature = request.signature


            };
            await _datagramRepo.AddAsync(datagram);
            return new AddDatagramResponse(id);
        }
    }
}
