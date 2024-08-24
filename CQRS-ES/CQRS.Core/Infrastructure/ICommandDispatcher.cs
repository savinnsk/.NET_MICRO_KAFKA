
using CQRS.Core.Commands;

namespace CQRS.Core.Infrastructure
{
    internal interface ICommandDispatcher
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;
        Task SendAsync(BaseCommand command);
    }
}
