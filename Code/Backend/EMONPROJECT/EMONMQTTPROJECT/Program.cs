using EMONMQTTPROJECT.MqttClients;
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
            DatagramClient client = new DatagramClient();
            await client.StartAsync(CancellationToken.None);
            TempratureClient tempratureClient = new TempratureClient();
            await tempratureClient.StartAsync(CancellationToken.None);



            Console.ReadKey();

        }
    }
}
