using Contract.Messages;

namespace Consumer.Handlers;

public interface IMessageHandler
{
    public Task HandleAsync(IMessage message);

    public Type MessageType { get; }
}