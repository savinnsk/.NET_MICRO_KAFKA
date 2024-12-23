using Message.Common.Events;
using Post.Common.Events;

namespace Post.Query.Infrastruture.Handlers
{
    public interface IEventHandler
    {
        Task On(PostCreatedEvent @event);
        Task On(PostMessageUpdatedEvent @event);
        Task On(PostLikedEvent @event);
        Task On(PostDeleteEvent @event);
        Task On(CommentAddedEvent @event);
        Task On(CommentUpdatedEvent @event);
        Task On(CommentRemoveEvent @event);
    }
}