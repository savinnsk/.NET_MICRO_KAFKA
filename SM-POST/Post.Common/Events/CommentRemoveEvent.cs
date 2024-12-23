using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class CommentRemoveEvent: BaseEvent
    {
        public Guid CommentId { get; set; }

        public  CommentRemoveEvent () : base(nameof(CommentRemoveEvent))
        {
        }
    }
}


