using CQRS.Core.Events;

namespace Message.Common.Events
{
    public class MessageUpdatedEvent : BaseEvent
    {

        public string Message { get; set; }
        public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent))
        {
        }
    }
}
