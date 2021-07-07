using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusFunctionTrigger
{
    public interface ICustomerImport
    {
        Task HandleMessage(string queueMsg, System.Collections.Generic.IDictionary<string, object> userProperties);
    }
}
