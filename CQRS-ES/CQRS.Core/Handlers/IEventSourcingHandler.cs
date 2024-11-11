
using CQRS.Core.Domain;

namespace CQRS.Core.Handlers
{
    public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(AgregateRoot aggregate);
        Task<T> GetByIdAsync(Guid  aggregateId);
    }
}
