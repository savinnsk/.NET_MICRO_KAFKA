using CQRS.Core.Events;

namespace Message.Common.Events
{
    public class PostMessageUpdatedEvent : BaseEvent
    {

        public string Message { get; set; }
        public PostMessageUpdatedEvent() : base(nameof(PostMessageUpdatedEvent))
        {
        }
    }
}
