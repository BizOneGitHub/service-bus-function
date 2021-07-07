using System;
using System.Collections.Generic;
using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

[assembly: FunctionsStartup(typeof(ServiceBusFunctionTrigger.Startup))]

namespace ServiceBusFunctionTrigger
{
    public class Startup : FunctionsStartup
    {

        IConfiguration Configuration { get; set; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            Configuration = builder.GetContext().Configuration;

            builder.Services.AddMicrosoftIdentityWebApiAuthentication(Configuration, "AzureAd");

            builder.Services.AddSingleton<ICustomerImport, CustomerImport>();

            // Add ServiceBus Client, can be done better....
            var connectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            builder.Services.AddSingleton<ServiceBusClient>(new ServiceBusClient(connectionString));
        }
    }
}
