namespace CQRS.Core.Commands
{
    public class RemovePostCommentCommand : BaseCommand
    {
       public required Guid CommentId {get;set;}
       public required string Username {get;set;} 
    }
}