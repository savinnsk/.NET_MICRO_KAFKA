using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class PostDeleteEvent: BaseEvent
    {
        public Guid CommentId { get; set; }

        public  PostDeleteEvent () : base(nameof(PostDeleteEvent))
        {
        }
    }
}



