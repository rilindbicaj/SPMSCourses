using Application.Events;
using Application.MessageBusClient;
using System.Text.Json;

namespace Application.EventPublisher
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IMessageBusClient _client;

        public EventPublisher(IMessageBusClient client)
        {
            _client = client;
        }

        public void PublishStudentGradedEvent(StudentGradedEvent eventData)
        {
            var message = JsonSerializer.Serialize(eventData);
            _client.SendMessage(message);
        }
    }
}