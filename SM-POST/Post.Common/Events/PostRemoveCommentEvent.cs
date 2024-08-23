using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class PostRemoveCommentEvent: BaseEvent
    {
        public Guid CommentId { get; set; }

        public  PostRemoveCommentEvent (string type) : base(nameof(PostRemoveCommentEvent))
        {
        }
    }
}


