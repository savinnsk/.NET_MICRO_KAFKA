using CQRS.Core.Command;

namespace Post.Cmd.Api.Commands
{

    public class EditMessageCommand : BaseCommand
    {
        public required string Message { get; set; }
    }
}