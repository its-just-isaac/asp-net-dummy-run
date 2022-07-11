using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Contract.Messages;

namespace Publisher;

public class SqsPublisher
{
    private readonly IAmazonSQS _sqs;

    public SqsPublisher(IAmazonSQS sqs)
    {
        _sqs = sqs;
    }

    public async Task PublishAsync<T>(string queueName, T message)
        where T : IMessage
    {
        string? queueUrl = (await _sqs.GetQueueUrlAsync(queueName)).QueueUrl;
        var request = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    // these attributes will be check by the consumer to determine whether to process or not
                    nameof(IMessage.MessageTypeName), new MessageAttributeValue
                    {
                        StringValue = message.MessageTypeName,
                        DataType = "String"
                    }
                }
            }
        };
        await _sqs.SendMessageAsync(request);
    }
}