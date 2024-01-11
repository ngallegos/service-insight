using System.Text.Json.Serialization;

namespace ServiceInsight.Web.Model;

public class MessageInfo
{
    [JsonPropertyName("id")]
    public string ID { get; set; }
    [JsonPropertyName("message_id")]
    public string MessageID { get; set; }
    [JsonPropertyName("message_type")]
    public string MessageType { get; set; }
    [JsonPropertyName("sending_endpoint")]
    public EndpointDetails SendingEndpoint { get; set; }
    [JsonPropertyName("receiving_endpoint")]
    public EndpointDetails ReceivingEndpoint { get; set; }
    [JsonPropertyName("time_sent")]
    public DateTimeOffset TimeSent { get; set; }
    [JsonPropertyName("processed_at")]
    public DateTimeOffset ProcessedAt { get; set; }
    [JsonPropertyName("critical_time")]
    public string CriticalTime { get; set; }
    [JsonPropertyName("processing_time")]
    public TimeSpan ProcessingTime { get; set; }
    [JsonPropertyName("delivery_time")]
    public TimeSpan DeliveryTime { get; set; }
    [JsonPropertyName("is_system_message")]
    public bool IsSystemMessage { get; set; }
    [JsonPropertyName("conversation_id")]
    public string ConversationID { get; set; }
    [JsonPropertyName("headers")] 
    public List<MessageHeader> Headers { get; set; } = new List<MessageHeader>();
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("message_intent")]
    public string Intent { get; set; }
    [JsonPropertyName("body_url")]
    public string BodyUrl { get; set; }
    [JsonPropertyName("body_size")]
    public int BodySize { get; set; }
    [JsonPropertyName("instance_id")]
    public string InstanceID { get; set; }
}

public class MessageHeader
{
    [JsonPropertyName("key")]
    public string Key { get; set; }
    [JsonPropertyName("value")]
    public string Value { get; set; }
}