using Amazon.SQS;
using Contract.Messages;
using Publisher;

var sqsClient = new AmazonSQSClient();

var publisher = new SqsPublisher(sqsClient);

await publisher.PublishAsync("customers", new CustomerCreated
{
    Id = 1,
    FullName = "Foo Bar"
});