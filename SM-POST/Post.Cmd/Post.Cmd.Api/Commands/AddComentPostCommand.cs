using CQRS.Core.Command;

namespace Post.Cmd.Api.Commands 
{

    public class AddPostCommentCommand : BaseCommand
    {
        public required string Comment {get;set;}
        public required string Username {get;set;}        
    }
}