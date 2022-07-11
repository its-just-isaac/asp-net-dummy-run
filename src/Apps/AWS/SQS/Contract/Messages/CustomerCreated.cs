using System.Text.Json.Serialization;

namespace Contract.Messages;

public class CustomerCreated : IMessage
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("fullName")] public string? FullName { get; init; }

    [JsonIgnore] public string MessageTypeName => nameof(CustomerCreated);
}