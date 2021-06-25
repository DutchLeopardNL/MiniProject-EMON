using EMONAPI.Persistance.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Domain.Temprature
{
    public interface ITempratureRepository
    {
        Task<TempratureModel> addAsync(TempratureModel temprature);
        Task<IEnumerable<TempratureModel>> getTempratures(CancellationToken cancellationToken);
    }
}
