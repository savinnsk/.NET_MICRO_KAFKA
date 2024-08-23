namespace CQRS.Core.Commands
{
    public class AddPostCommentCommand : BaseCommand
    {
        public required string Comment {get;set;}
        public required string Username {get;set;}        
    }
}