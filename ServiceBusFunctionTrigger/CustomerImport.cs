using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ServiceBusFunctionTrigger
{
    public class CustomerImport : ICustomerImport
    {
        private readonly ILogger<CustomerImport> _log;
        private readonly ServiceBusClient _serviceBusClient;

        public CustomerImport(ILogger<CustomerImport> log, ServiceBusClient serviceBusClient)
        {
            _log = log;
            _serviceBusClient = serviceBusClient;
        }

        public async Task HandleMessage(string bodyMsg, System.Collections.Generic.IDictionary<string, object> userProperties)
        {

            // Get from queue
            /* dynamic message = JToken.Parse(queueMsg);
             string urlToCusomerBase = message?.Message?.Url + message?.Message?.RecordID;
             string action = message?.ActionType;
             var messageType = userProperties.FirstOrDefault(p => p.Key.ToLower() == "messagetype").Value?.ToString();
             string body = $"urlToCusomerBase: {urlToCusomerBase} action: {action}  messageType: {messageType}";
             Console.WriteLine(body);*/
           
             await SendMessageToTopicChangeParentAsync(bodyMsg);

        }

        private async Task SendMessageToTopicChangeParentAsync(string bodyMsg)
        {
            try
            {

                string topicName = Environment.GetEnvironmentVariable("ChangeServiceBusTopic");
                     Console.WriteLine($"ChangeServiceBusTopic {topicName}");
                ServiceBusSender sender = _serviceBusClient.CreateSender(topicName);
                await sender.SendMessageAsync(new ServiceBusMessage(bodyMsg));
            }
            catch (System.Exception ex)
            {
                _log.LogError($"Could not send to Change parent Topic");
                throw new Exception("Could not send to Change parent Topic", ex);
            }
        }
    }
}
