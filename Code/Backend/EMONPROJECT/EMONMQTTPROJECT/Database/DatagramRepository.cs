using EMONAPI.Domain.Datagram;
using EMONAPI.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EMONMQTTPROJECT.Database
{
    class DatagramRepository : IDatagramRepository
    {
        static HttpClient client = new HttpClient();
        public async Task<FullDatagram> AddAsync(FullDatagram datagram)
        {
            if (datagram == null)
            {
                throw new ArgumentNullException("datagram was null");
            }
            else
            {
                datagram.Id = Guid.NewGuid().ToString();
                string jsonString = JsonSerializer.Serialize(datagram);
                var content = new StringContent(datagram.ToString(), Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://localhost:44371/api/Datagram/PostDatagram", content);
                Console.WriteLine("Status Code:  " + result.StatusCode);
                return datagram;
            }
        }

        public Task<IEnumerable<FullDatagram>> GetDatagrams(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
