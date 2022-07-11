using Amazon;
using Amazon.SQS;
using Consumer;
using Consumer.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddHostedService<SqsConsumerService>();
builder.Services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(RegionEndpoint.APSouth1));
builder.Services.AddSingleton<MessageDispatcher>();
builder.Services.AddMessageHandlers();

app.MapGet("/", () => "Hello World!");

app.Run();