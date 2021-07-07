/*using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusFunctionTrigger
{
    public static class ServiceBusDemoFunction
    {
        [FunctionName("ServiceBusDemoFunction")]
        public static void Run([ServiceBusTrigger("mytopic", "sbs-test", Connection = "ServiceBusConnectionString")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}*/
