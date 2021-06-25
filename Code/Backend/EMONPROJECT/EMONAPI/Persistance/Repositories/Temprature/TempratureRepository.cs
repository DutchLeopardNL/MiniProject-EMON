using EMONAPI.Domain.Temprature;
using EMONAPI.Persistance.Context;
using EMONAPI.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMONAPI.Persistance.Repositories
{
    public class TempratureRepository : ITempratureRepository
    {
        private readonly MeterContext _meterContext;

        public TempratureRepository(MeterContext meterContext)
        {
            _meterContext = meterContext;
        }
        public async Task<TempratureModel> addAsync(TempratureModel temprature)
        {
            if (temprature == null)
            {
                throw new ArgumentNullException("temprature was null");
            }
            else
            {
                try
                {
                    await _meterContext.AddAsync(temprature);
                    await _meterContext.SaveChangesAsync();
                    return temprature;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public async Task<IEnumerable<TempratureModel>> getTempratures(CancellationToken cancellationToken)
        {
            var data = await _meterContext.tempratures.ToListAsync();
            return data;
        }
    }
}
