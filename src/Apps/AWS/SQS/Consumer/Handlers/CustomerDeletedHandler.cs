using Contract.Messages;

namespace Consumer.Handlers;

public class CustomerDeletedHandler : IMessageHandler
{
    private readonly ILogger<CustomerDeletedHandler> _logger;

    public CustomerDeletedHandler(ILogger<CustomerDeletedHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(IMessage message)
    {
        var customerDeleted = (CustomerDeleted) message;
        _logger.LogInformation("Customer deleted with ID: {Id}", customerDeleted.Id);
        return Task.CompletedTask;
    }

    public Type MessageType => typeof(CustomerDeleted);
}