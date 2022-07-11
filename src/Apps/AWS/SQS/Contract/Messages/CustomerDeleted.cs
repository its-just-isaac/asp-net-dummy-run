using System.Text.Json.Serialization;

namespace Contract.Messages;

public class CustomerDeleted : IMessage
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonIgnore] public string MessageTypeName => nameof(CustomerDeleted);
}