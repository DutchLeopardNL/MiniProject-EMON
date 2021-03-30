using MQTTReceiver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMONMQTTPROJECT
{
    class Program
    {
        static async Task Main(string[] args)
        {
            LocalMqttClient client = new LocalMqttClient();
            await client.StartAsync(CancellationToken.None);



            Console.ReadKey();

        }
    }
}
