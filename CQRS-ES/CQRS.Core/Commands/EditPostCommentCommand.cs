
namespace CQRS.Core.Commands
{
    public class EditPostCommentCommand : BaseCommand
    {
        public required Guid CommentId { get; set; }
        public required string Comment { get; set; }
        public required string UserName { get; set; }
    }

}
