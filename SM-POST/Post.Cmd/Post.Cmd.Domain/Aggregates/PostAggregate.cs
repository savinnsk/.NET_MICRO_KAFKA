using CQRS.Core.Domain;
using Message.Common.Events;
using Post.Common.Events;

namespace Post.Cmd.Domain.Aggregates
{
    public class PostAggregate : AgregateRoot
    {
        private bool _active;
        private string _author;
        private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

        public bool Active
        {
            get => _active;set => _active = value;
        }

        public PostAggregate()
        {
           
        }

        public PostAggregate(Guid id,string author, string message) 
        {

            RaiseEvent(new PostCreatedEvent
            {
                Id = id,
                Author = author,
                Message = message,
                DatePosted = DateTime.Now
            });

        }

        public void Apply(PostCreatedEvent @event) 
        {
            _id = @event.Id;
            _author = @event.Author;
            _active = true;
        }

        public void EditMessage(string message)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit the message of an inactive post!");
            }

            if (string.IsNullOrWhiteSpace(message)){
                throw new InvalidOperationException($"The value of {nameof(message)} cannot be null or empty. Please provide a valid {nameof(message)}");
            }

            RaiseEvent(new MessageUpdatedEvent
            {
                Id = _id,
                Message = message
            });
        }

        public void Apply(MessageUpdatedEvent @event) {
            _id = @event.Id;

        }


        public void LikePost() {
            if (!_active) {
                throw new InvalidOperationException("You cannot like an inactive post!"); 
            }
            RaiseEvent(new PostLikedEvent
            {
                Id = _id
            });
            
        }

        public void Apply(PostLikedEvent @event) 
        {
            _id = @event.Id;     
        }

        public void AddComment(string comment, string username) 
        {
            if (!_active) {
                throw new InvalidOperationException("You cannot like an inactive post!");
            }

            if (string.IsNullOrWhiteSpace(comment)) {
                throw new InvalidOperationException($"THe value of {nameof(comment)} cannot be null or empty. Please provide a valid {nameof(comment)}!");
            }

            RaiseEvent(new PostCommentAddedEvent { 
                Id = _id, 
                Username = username,
                Comment = comment,
                CommentId = Guid.NewGuid(),
                CommentDate = DateTime.Now
            });
        }

        public void Apply(PostCommentAddedEvent @event) 
        {
            _id = @event.Id;
            _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.Username));
        }

        public void EditComment(Guid commentId, string comment, string username)
        {

            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit a comment of an inactive post!");
            }

            if (!_comments[commentId].Item2.Equals(username,StringComparison.CurrentCultureIgnoreCase)) {
                throw new InvalidOperationException("you are not allowe to edit a comment that was made by another user ");
            };


            RaiseEvent(new PostCommentUpdatedEvent {
                Id= _id,
                CommentId= commentId,
                Username= username,
                DateEdited = DateTime.Now,
                Comment = comment
            });
        }

        public void Apply(PostCommentUpdatedEvent @event)
        {
            _id = @event.Id;
            _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment,@event.Username);
        
        }

        public void RemoveComment(Guid commentId, string username)
        {

             if (!_active)
            {
                throw new InvalidOperationException("You cannot edit a comment of an inactive post!");
            }


            if (!_comments[commentId].Item2.Equals(username,StringComparison.CurrentCultureIgnoreCase)) {
                throw new InvalidOperationException("you are not allowed to remove a comment that was made by another user ");
            };



            RaiseEvent(new PostRemoveCommentEvent
            {
                Id = _id,
                CommentId = commentId

            });


        }


        public void Apply(PostRemoveCommentEvent @event) {
            _id = @event.Id;
            _comments.Remove(@event.CommentId); 
        }


        public void DeletePost(string username)
        {
            if (!_active) { 
                throw new InvalidOperationException("The post has alredy been removed");
            }

            if (!_author.Equals(username, StringComparison.CurrentCultureIgnoreCase)) { 
                throw new InvalidOperationException("You are not allowed to deletet this post");
            }

            RaiseEvent(new PostDeleteEvent 
            { 
                Id = _id,
            });

        }


        public void Apply(PostDeleteEvent @event) {
            _id = @event.Id;
            _active = false;
        }



    }
}       
