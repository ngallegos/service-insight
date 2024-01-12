using System.Text.Json.Serialization;

namespace ServiceInsight.Web.Model;

public class SagaInfo
{
    [JsonPropertyName("saga_id")]
    public string SagaID { get; set; }
    
    [JsonPropertyName("saga_type")]
    public string SagaType { get; set; }
    
    [JsonPropertyName("change_status")]
    public string ChangeStatus { get; set; }
}

public class SagaDetail : SagaInfo
{
    [JsonPropertyName("id")]
    public string ID { get; set; }
    
    [JsonPropertyName("changes")]
    public List<SagaChange> Changes { get; set; } = new List<SagaChange>();
    
}

public class SagaChange
{
    [JsonPropertyName("start_time")]
    public DateTimeOffset StartTime { get; set; }
    [JsonPropertyName("finish_time")]
    public DateTimeOffset FinishTime { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("state_after_change")]
    public string StateAfterChange { get; set; }
    [JsonPropertyName("endpoint")]
    public string Endpoint { get; set; }
    [JsonPropertyName("initiating_message")]
    public SagaChangeInitiatingMessage InitiatingMessage { get; set; }
    [JsonPropertyName("outgoing_messages")]
    public List<SagaChangeOutgoingMessage> OutgoingMessages { get; set; } = new List<SagaChangeOutgoingMessage>();
}

public class SagaChangeInitiatingMessage : MessageBase
{
    [JsonPropertyName("is_saga_timeout_message")]
    public bool IsSagaTimeoutMessage { get; set; }
    [JsonPropertyName("originating_endpoint")]
    public string OriginatingEndpoint { get; set; }
    [JsonPropertyName("originating_machine")]
    public string OriginatingMachine { get; set; }
    [JsonPropertyName("intent")]
    public string Intent { get; set; }
}

public class SagaChangeOutgoingMessage
{
    [JsonPropertyName("destination")]
    public string Destination { get; set; }
    [JsonPropertyName("intent")]
    public string Intent { get; set; }
}