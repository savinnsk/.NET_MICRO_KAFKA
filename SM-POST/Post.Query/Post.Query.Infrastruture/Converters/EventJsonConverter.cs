using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Events;
using Message.Common.Events;
using Post.Common.Events;

public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(BaseEvent).IsAssignableFrom(typeToConvert);
    }

    public override BaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if(!JsonDocument.TryParseValue(ref reader,out var doc))
        {
            throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
        }

        if(!doc.RootElement.TryGetProperty("Type", out var type))
        {
            throw new JsonException($"Could not find Type discriminator!");
        }

        var typeDiscriminator = type.GetString();
        var json = doc.RootElement.GetRawText();

        return typeDiscriminator switch {
            nameof(PostCreatedEvent) => JsonSerializer.Deserialize<PostCreatedEvent>(json, options),
            nameof(PostMessageUpdatedEvent) => JsonSerializer.Deserialize<PostMessageUpdatedEvent>(json, options),
            nameof(PostLikedEvent) => JsonSerializer.Deserialize<PostLikedEvent>(json, options),
            nameof(PostDeleteEvent) => JsonSerializer.Deserialize<PostDeleteEvent>(json, options),
            nameof(CommentAddedEvent) => JsonSerializer.Deserialize<CommentAddedEvent>(json, options),
            nameof(CommentUpdatedEvent) => JsonSerializer.Deserialize<CommentUpdatedEvent>(json, options),
            nameof(CommentRemoveEvent) => JsonSerializer.Deserialize<CommentRemoveEvent>(json, options),
            _ => throw new JsonException($"Unknown event type {typeDiscriminator}")
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}