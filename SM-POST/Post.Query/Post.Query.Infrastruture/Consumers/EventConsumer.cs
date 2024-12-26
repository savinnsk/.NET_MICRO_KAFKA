using System.Text.Json;
using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using Post.Query.Infrastruture.Handlers;

namespace Post.Query.Infrastruture.Consumers;

public class EventConsumer : IEventConsumer
{
    public readonly ConsumerConfig _config;
    public readonly IEventHandler _eventHandler;

    public EventConsumer(IOptions<ConsumerConfig> config , IEventHandler _eventHandler )
    {
        _config = config.Value;
        _eventHandler = _eventHandler;
    }
    public void Consume(string topic)
    {
        using var consumer = new ConsumerBuilder<string, string>(_config)
        .SetKeyDeserializer(Deserializers.Utf8).
        SetValueDeserializer(Deserializers.Utf8).
        Build();

        consumer.Subscribe(topic);
        while(true)
        {
            var consumeResult = consumer.Consume();

            if(consumeResult?.Message is null)  continue;

            var options = new JsonSerializerOptions{ Converters = {new EventJsonConverter()}};

            var @event = JsonSerializer.Deserialize<BaseEvent>(consumeResult.Message.Value, options);

            var handlerMethod = _eventHandler.GetType().GetMethod("On", new Type[]{@event.GetType()});

            if(handlerMethod is null) throw new Exception($"{nameof(handlerMethod)} could not find event handler method!");

            handlerMethod.Invoke(_eventHandler, new object[]{@event});

            consumer.Commit(consumeResult);
        }


    }
}