using EMONAPI.Domain.Temprature;
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
    class TempratureRepository : ITempratureRepository
    {
        static HttpClient client = new HttpClient();

        public async Task<TempratureModel> addAsync(TempratureModel temprature)
        {
            if (temprature == null)
            {
                throw new ArgumentNullException("temprature was null");
            }
            else
            {
                temprature.Id = Guid.NewGuid().ToString();
                string jsonString = JsonSerializer.Serialize(temprature);
                var content = new StringContent(temprature.ToString(), Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://localhost:44371/api/Temprature/PostTemprature", content);
                Console.WriteLine("Status Code:  " + result.StatusCode);
                return temprature;
            }
        }

        public Task<IEnumerable<TempratureModel>> getTempratures(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
