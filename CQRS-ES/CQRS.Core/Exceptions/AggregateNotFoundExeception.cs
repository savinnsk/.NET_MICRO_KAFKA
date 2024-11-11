
namespace CQRS.Core.Exceptions
{
    public class AggregateNotFoundExeception : Exception
    {
        public AggregateNotFoundExeception(string message) : base(message) { }
    }
}
