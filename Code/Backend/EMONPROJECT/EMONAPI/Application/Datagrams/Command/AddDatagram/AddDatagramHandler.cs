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
            FullDatagram datagram = new FullDatagram();
            if(request.currentUsage > 10)
            {
                datagram.Id = id;
                datagram.timeStamp = DateTime.Now.ToString("dd_MMM_yyyy_HH_mm_ss");
                datagram.currentUsage = request.currentUsage / 1000;
                datagram.totalLow = request.totalLow / 1000;
                datagram.totalHigh = request.totalHigh / 1000;
                datagram.returnLow = request.returnLow / 1000;
                datagram.returnHigh = request.returnHigh / 1000;
                datagram.gasUsage = request.gasUsage / 1000;
                datagram.signature = request.signature;
            }
            else
            {
                    datagram.Id = id;
                    datagram.timeStamp = DateTime.Now.ToString("dd_MMM_yyyy_HH_mm_ss");
                    datagram.currentUsage = request.currentUsage;
                    datagram.totalLow = request.totalLow;
                    datagram.totalHigh = request.totalHigh;
                    datagram.returnLow = request.returnLow;
                    datagram.returnHigh = request.returnHigh;
                    datagram.gasUsage = request.gasUsage;
                    datagram.signature = request.signature;
            }
           
            await _datagramRepo.AddAsync(datagram);
            return new AddDatagramResponse(id);
        }
    }
}
