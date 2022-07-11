using System.Net;
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Contract.Messages;

namespace Consumer;

public class SqsConsumerService : BackgroundService
{
    private readonly IAmazonSQS _sqs;
    private readonly IConfiguration _configuration;
    private readonly MessageDispatcher _messageDispatcher;
    private readonly List<string> _messageAttributeNames = new() {"All"};

    public SqsConsumerService(IAmazonSQS sqs, IConfiguration configuration, MessageDispatcher messageDispatcher)
    {
        _sqs = sqs;
        _configuration = configuration;
        _messageDispatcher = messageDispatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string? queueUrl = (await _sqs.GetQueueUrlAsync(_configuration["queueName"], stoppingToken)).QueueUrl;
        var receiveRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            // ask for specific attribute name
            MessageAttributeNames = _messageAttributeNames,
            AttributeNames = _messageAttributeNames
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var messageResponse = await _sqs.ReceiveMessageAsync(receiveRequest, stoppingToken);
            if (messageResponse.HttpStatusCode != HttpStatusCode.OK)
            {
                // TODO: do some logging or handling?
                continue;
            }

            foreach (var message in messageResponse.Messages)
            {
                // received message must have an attribute
                string? messageTypeName = message.MessageAttributes.GetValueOrDefault(nameof(IMessage.MessageTypeName))?.StringValue;

                if (messageTypeName is null)
                {
                    continue;
                }

                // check if message type is defined on the consumer side 
                if (!_messageDispatcher.CanHandleMessageType(messageTypeName))
                {
                    // TODO: handle not found message type
                    continue;
                }

                var messageType = _messageDispatcher.GetMessageTypeByName(messageTypeName)!;
                var messageAsType = (IMessage) JsonSerializer.Deserialize(message.Body, messageType)!;

                await _messageDispatcher.DispatchAsync(messageAsType);
                await _sqs.DeleteMessageAsync(queueUrl, message.ReceiptHandle, stoppingToken);
            }
        }
    }
}