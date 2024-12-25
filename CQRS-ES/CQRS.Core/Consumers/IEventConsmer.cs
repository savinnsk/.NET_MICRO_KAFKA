namespace CQRS.Core.Consumers;

public interface IEventConsmer
{

    void Consume(string topic);
    
}