using EMONAPI.Domain.Temprature;
using EMONAPI.Persistance.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Application.Tempratures.Command.AddTemprature
{
    public class AddTempratureHandler : IRequestHandler<AddTempratureCommand,AddTempratureResponse>
    {
        private readonly ITempratureRepository _tempratureRepo;
        public AddTempratureHandler(ITempratureRepository tempratureRepository)
        {
            _tempratureRepo = tempratureRepository;
        }
        public async Task<AddTempratureResponse> Handle(AddTempratureCommand request, CancellationToken cancellation)
        {
            var id = Guid.NewGuid().ToString();
            TempratureModel temprature = new TempratureModel
            {
                Id = id,
                timeStamp = DateTime.Now.ToString("dd_MMM_yyyy_HH_mm_ss"),
                value = request.value


            };
            await _tempratureRepo.addAsync(temprature);
            return new AddTempratureResponse(id);
        }
    }
}
