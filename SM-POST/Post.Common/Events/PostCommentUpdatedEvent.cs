using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class PostCommentUpdatedEvent : BaseEvent
    {
        public string Username { get; set; }
        public Guid CommentId { get; set; }
        public string Comment { get; set; }
        public DateTime DateEdited { get; set; }

        public PostCommentUpdatedEvent() : base(nameof(PostCommentUpdatedEvent))
        {
        }
    }
}

