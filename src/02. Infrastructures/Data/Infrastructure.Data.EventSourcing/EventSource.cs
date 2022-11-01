using EventStore.ClientAPI;
using Framework.Domain.Data;
using Framework.Domain.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EventSourcing
{
    public class EventSource : IEventSource
    {
        private readonly IEventStoreConnection _connection;
        public EventSource(IEventStoreConnection connection)
        {
            _connection = connection;
            _connection.ConnectAsync().Wait();
        }
        public void Save<TEvent>(string aggregateName, string streamId, IEnumerable<TEvent> events)
            where TEvent : IEvent
        {
            if (!events.Any()) return;
            var changes = events
                   .Select(@event =>
                       new EventData(
                           eventId: Guid.NewGuid(),
                           type: @event.GetType().Name,
                           isJson: true,
                           data: Serialize(@event),
                           metadata: Serialize(new EventMetadata
                           { ClrType = @event.GetType().AssemblyQualifiedName })
                       ))
                   .ToArray();
            if (!changes.Any()) return;
            var streamName = $"{aggregateName} - {streamId}";
            _connection.AppendToStreamAsync(
                streamName,
                ExpectedVersion.Any,
                changes).Wait();
        }

        private static byte[] Serialize(object data) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }
    internal class EventMetadata
    {
        public string ClrType { get; set; }
    }
}
