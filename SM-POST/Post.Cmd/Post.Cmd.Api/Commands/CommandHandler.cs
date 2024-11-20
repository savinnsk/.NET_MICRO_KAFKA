using CQRS.Core.Handlers;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Api.Commands;

public class CommandHandler : ICommandHandler
{
    private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }
    
    public async Task HandlerAsync(NewPostCommand command)
    {
       var aggregate = new PostAggregate(command.Id, command.Author, command.Message); 
       await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandlerAsync(AddPostCommentCommand command)
    {
       var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id); 
       aggregate.AddComment(command.Comment, command.Username);
       await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandlerAsync(DeletePostCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id); 
        aggregate.DeletePost(command.Username);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandlerAsync(LikePostCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.LikePost();
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandlerAsync(RemovePostCommentCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id); 
        aggregate.RemoveComment(command.CommentId, command.Username);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandlerAsync(EditPostCommentCommand command)
    {
       var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id); 
       aggregate.EditComment(command.CommentId, command.Comment, command.Username);
       await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandlerAsync(EditMessageCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.EditMessage(command.Message);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}