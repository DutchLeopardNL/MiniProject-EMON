using EMONAPI.Persistance.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Domain.Datagram
{
    public interface IDatagramRepository
    {
        Task<FullDatagram> AddAsync(FullDatagram datagram);
        Task<IEnumerable<FullDatagram>> GetDatagrams(CancellationToken cancellationToken);
    }
}
