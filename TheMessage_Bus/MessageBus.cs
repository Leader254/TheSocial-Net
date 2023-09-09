using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMessage_Bus
{
    public class MessageBus : IMessageBus
    {
        public string ConnectionString = "Endpoint=sb://commerce.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=R+X2kVe8La0bkm9CG5cp5gSZ3yNvHM9Pb+ASbE8TpUU=";
        public async Task PublishMessage(object message, string queue_topic_name)
        {
            var serviceBus = new ServiceBusClient(ConnectionString);
            var sender = serviceBus.CreateSender(queue_topic_name);
            var jsonMessage = JsonConvert.SerializeObject(message);

            var theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                // create a unique message id
                CorrelationId = Guid.NewGuid().ToString(),
            };
            // send the message
            await sender.SendMessageAsync(theMessage);
            // close the sender
            await sender.DisposeAsync();
        }
    }
}
