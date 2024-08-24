
using CQRS.Core.Commands;
using CQRS.Core.Infrastructure;

namespace Post.Cmd.Infrastruture.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();//new Dictionary<Type, Func<BaseCommand, Task>>(); 

        public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                throw new IndexOutOfRangeException("You cannot register the same command handler twice!");
            }

            _handlers.Add(typeof(T), x => handler((T)x));

        }

        public async Task SendAsync(BaseCommand command)
        {
            //callback
            if (_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
            {
                await handler(command);
            }
            else
            {
                throw new ArgumentException(nameof(handler), "No Command hadler was registered");
            }
        }
    }
}
