using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EMONAPI.Persistance.Context;
using EMONAPI.Persistance.Entities;
using EMONMQTTPROJECT.Database;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;

namespace EMONMQTTPROJECT.MqttClients
{
    class TempratureClient
    {
        private MeterContext context = new MeterContext();
        private TempratureRepository repository = new TempratureRepository();
        private IMqttClient client;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("test.mosquitto.org")
                .WithClientId(Guid.NewGuid().ToString())
                .WithCleanSession()
                .Build();


            client = new MqttFactory().CreateMqttClient();

            client.UseConnectedHandler(/*async*/ async e =>
            {
                Console.WriteLine("### CONNECTED WITH BROKER ###");
                await this.SubscribeTopic("EMON_CHANNEL_2");
                client.UseApplicationMessageReceivedHandler(b =>
                {
                    HandleMessageReceived(b.ApplicationMessage);
                });

            });
            await client.ConnectAsync(options);
        }

        public async Task SubscribeTopic(string topic)
        {
            var subscribeResult = await client.SubscribeAsync(topic);

            Console.WriteLine("### SUBSCRIBED ###");
            Console.WriteLine("### Result: " + subscribeResult.Items.FirstOrDefault()?.ResultCode);
            Console.WriteLine("### Result: " + subscribeResult.Items.FirstOrDefault()?.TopicFilter);
        }

        private void HandleMessageReceived(MqttApplicationMessage message)
        {
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            Console.WriteLine($"+ Topic = {message.Topic}");

            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(message.Payload)}");
            Console.WriteLine($"+ QoS = {message.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {message.Retain}");
            Console.WriteLine();
            string payload = Encoding.UTF8.GetString(message.Payload);
            Console.WriteLine(payload);
            TempratureModel tempratureModel = new TempratureModel();
            tempratureModel.Id = Guid.NewGuid().ToString();
            tempratureModel.timeStamp = DateTime.Now.ToString("MMM_dd_yyyy_HH_mm_ss");
                tempratureModel.value = payload;
                context.tempratures.Add(tempratureModel);
                context.SaveChanges();
                Console.WriteLine("Temprature added"); 
        }
        public void saveTemprature(TempratureModel temprature)
        {
            addScantoDB(temprature);
        }

        public async void addScantoDB(TempratureModel temprature)
        {
            await repository.addAsync(temprature);
        }
    }
}

