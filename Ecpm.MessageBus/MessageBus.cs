using System.Text;
using Azure.Messaging.ServiceBus;

namespace Ecom.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly string _connectionString;
        public async Task PublishMessage(object message, string topic_queue_name = null)
        {
            string connectionString = "Endpoint=sb://ecpm-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Z6Q5Q5Q5Q5";
            await using var client = new ServiceBusClient(connectionString);

            var sender = client.CreateSender(topic_queue_name);
            var jsonMessage = System.Text.Json.JsonSerializer.Serialize(message);

            var messageBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };
            await sender.SendMessageAsync(messageBusMessage);
            await client.DisposeAsync();
        }
    }
}
