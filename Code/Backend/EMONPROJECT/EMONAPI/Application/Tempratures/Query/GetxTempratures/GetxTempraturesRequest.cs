using MediatR;


namespace EMONAPI.Application.Tempratures.Query.GetxTempratures
{
    public class GetxTempraturesRequest : IRequest<GetxTempraturesResponse>
    {
        public int amount;
        public GetxTempraturesRequest(int _amount)
        {
            amount = _amount;
        }
    }
}
