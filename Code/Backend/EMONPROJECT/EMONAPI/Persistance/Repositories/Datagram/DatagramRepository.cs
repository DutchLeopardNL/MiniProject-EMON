using EMONAPI.Domain.Datagram;
using EMONAPI.Persistance.Context;
using EMONAPI.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Persistance.Repositories.Datagram
{
    public class DatagramRepository : IDatagramRepository
    {
        private readonly MeterContext _meterContext;
        public DatagramRepository(MeterContext meterContext)
        {
            _meterContext = meterContext;
        }
        public async Task<FullDatagram> AddAsync(FullDatagram datagram)
        {
           if(datagram == null)
            {
                throw new ArgumentNullException("datagram was null");
            }
            else
            {
                try
                {
                    await _meterContext.AddAsync(datagram);
                    await _meterContext.SaveChangesAsync();
                    return datagram;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public async Task<IEnumerable<FullDatagram>> GetDatagrams(CancellationToken cancellationToken)
        {
            var data = await _meterContext.datagrams.ToListAsync();
            return data;
        }
    }
}
