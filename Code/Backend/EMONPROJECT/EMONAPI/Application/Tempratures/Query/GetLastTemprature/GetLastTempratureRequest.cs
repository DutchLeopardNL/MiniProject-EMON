using MediatR;

namespace EMONAPI.Application.Tempratures.Query.GetLastTemprature
{
    public class GetLastTempratureRequest : IRequest<GetLastTempratureResponse>
    {
        public GetLastTempratureRequest()
        {

        }
    }
}
