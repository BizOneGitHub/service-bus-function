using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ServiceBusFunctionTrigger
{
    public class CrmErpAddressTopicTrigger
    {
        private readonly ILogger<CrmErpAddressTopicTrigger> _log;

        private readonly ICustomerImport _customerImport;

        public CrmErpAddressTopicTrigger(ILogger<CrmErpAddressTopicTrigger> log, ICustomerImport customerImport)
        {
            _log = log;
            _customerImport = customerImport;
        }

        [FunctionName("CrmErpAddressTopicTrigger")]
        public async Task Run([ServiceBusTrigger("mytopic", "sbs-test", Connection = "ServiceBusConnectionString", IsSessionsEnabled = false)] Message sbMessage
            , MessageReceiver messageReceiver
            // , string lockToken
            )
        {
            try
            {
                Console.WriteLine($"Process procpect message {Encoding.UTF8.GetString(sbMessage.Body)}");
                _log.LogInformation($"Process procpect message {sbMessage.Body}");
                await _customerImport.HandleMessage(Encoding.UTF8.GetString(sbMessage.Body), sbMessage.UserProperties);
            }
            catch (System.Exception ex)
            {
                _log.LogError(ex, "Error");
                //await messageReceiver.DeadLetterAsync(lockToken);
                throw new System.Exception("Error handling prospect");
            }
        }
    }
}
