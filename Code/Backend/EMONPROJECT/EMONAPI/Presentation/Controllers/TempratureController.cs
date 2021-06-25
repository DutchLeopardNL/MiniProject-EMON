using EMONAPI.Application.Tempratures.Command.AddTemprature;
using EMONAPI.Application.Tempratures.Query.GetLastTemprature;
using EMONAPI.Application.Tempratures.Query.GetxTempratures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TempratureController
    {
            private readonly IMediator _mediator;
            public TempratureController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpGet("lastTemprature")]
            public async Task<GetLastTempratureResponse> GetlastTemprature(CancellationToken cancellation)
            {
                var query = new GetLastTempratureRequest();
                var result = await _mediator.Send(query);
                return result;
            }
            [HttpGet("tempratures/{amount}")]
            public async Task<GetxTempraturesResponse> GetScanByAmount(CancellationToken cancellation, int amount)
            {
                var query = new GetxTempraturesRequest(amount);
                var result = await _mediator.Send(query);
                return result;
            }
            [HttpPost("PostTemprature")]
            public async Task<AddTempratureResponse> AddDatagram([FromBody] AddTempratureCommand command, CancellationToken cancellationToken) => await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        
    }
}
