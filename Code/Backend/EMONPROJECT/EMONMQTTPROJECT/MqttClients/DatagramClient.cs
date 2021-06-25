using EMONAPI.Persistance.Context;
using EMONAPI.Persistance.Entities;
using EMONMQTTPROJECT.Database;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace MQTTReceiver
{

    class DatagramClient
    {
        private MeterContext context = new MeterContext();
        private DatagramRepository repository = new DatagramRepository();
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
                await this.SubscribeTopic("EMON_CHANNEL_1");
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
            var testObject = JsonConvert.DeserializeObject<highestObject>(payload);

            FullDatagram datagram = new FullDatagram();
            datagram.signature = testObject.datagram.signature;
            datagram.Id = Guid.NewGuid().ToString();
            datagram.timeStamp = DateTime.Now.ToString("MMM_dd_yyyy_HH_mm_ss");
            foreach (var substring in testObject.datagram.p1.Split(new string[] { Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
            {
                if(substring.Contains("1-0:1.8.1"))
                {
                    double value = Convert.ToDouble(removeExcessData(substring.ToString(), "1-0:1.8.1", "*kWh"));
                    datagram.totalHigh = value;
                }
                else if(substring.Contains("1-0:1.8.2"))
                {
                    double value = Convert.ToDouble(removeExcessData(substring.ToString(), "1-0:1.8.2", "*kWh"));
                    datagram.totalLow = value;
                }
                else if (substring.Contains("1-0:1.7.0"))
                {
                    double value = Convert.ToDouble(removeExcessData(substring.ToString(), "1-0:1.7.0", "*kW"));
                    datagram.currentUsage = value;
                }
                else if (substring.Contains("1-0:2.8.1"))
                {
                    double value = Convert.ToDouble(removeExcessData(substring.ToString(), "1-0:2.8.1", "*kWh"));
                    datagram.returnHigh = value;
                }
                else if (substring.Contains("1-0:2.8.2"))
                {
                    double value = Convert.ToDouble(removeExcessData(substring.ToString(), "1-0:2.8.2", "*kWh"));
                    datagram.returnLow = value;
                }
                else if (substring.Contains("0-1:24.2.1"))
                {
                    double value = Convert.ToDouble(removeExcessData(substring.ToString(), "0-1:24.2.1", "*m3", "210330112500S"));
                    datagram.gasUsage = value;
                }
            }
            context.datagrams.Add(datagram);
            context.SaveChanges();
            Console.WriteLine("datagram added");

        }

        private string removeExcessData(string substring,string code,string unit)
        {
            substring = substring.Replace(code, "");
            substring = substring.Replace(unit, "");
            substring = substring.Replace("(", "");
            substring = substring.Replace(")", "");
            
            return substring;
        }

        private string removeExcessData(string substring, string code, string unit, string extraCode)
        {

            substring = substring.Split("(")[2];
            substring = substring.Replace(unit, "");
            substring = substring.Replace(")","");
            
            
            return substring;
        }

        public void saveDatagram(FullDatagram datagram)
        {
            addScantoDB(datagram);
        }

        public async void addScantoDB(FullDatagram datagram)
        {
            await repository.AddAsync(datagram);
        }
    }

}
