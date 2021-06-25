using EMONAPI.Application.Datagrams.Command.AddDatagram;
using EMONAPI.Application.Datagrams.Query.GetLastDatagram;
using EMONAPI.Application.Datagrams.Query.GetxDatagrams;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatagramController
    {
        private readonly IMediator _mediator;
        public DatagramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("lastDatagram")]
            public async Task<GetLastDatagramResponse> GetlastDatagram(CancellationToken cancellation)
        {
            var query = new GetLastDatagramRequest();
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet("datagrams/{amount}")]
        public async Task<GetxDatagramsResponse> GetScanByAmount(CancellationToken cancellation, int amount)
        {
            var query = new GetxDatagramsRequest(amount);
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpPost("PostDatagram")]
        public async Task<AddDatagramResponse> AddDatagram([FromBody] AddDatagramCommand command, CancellationToken cancellationToken) => await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
    }
}
