using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ServiceBusFunctionTrigger
{
    public class CiamOperationHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;
        public CiamOperationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    }
}
