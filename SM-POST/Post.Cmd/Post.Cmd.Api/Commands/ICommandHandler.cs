namespace Post.Cmd.Api.Commands 
{
    public interface ICommandHandler
    {
        Task HandlerAsync(NewPostCommand command);
        Task HandlerAsync(AddPostCommentCommand command);
        Task HandlerAsync(DeletePostCommand command);
        Task HandlerAsync(LikePostCommand command);
        Task HandlerAsync(RemovePostCommentCommand command);
        Task HandlerAsync(EditPostCommentCommand command);
        Task HandlerAsync(EditMessageCommand command);
    }
}