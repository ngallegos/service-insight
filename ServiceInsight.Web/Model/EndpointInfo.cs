using System.Text.Json.Serialization;

namespace ServiceInsight.Web.Model;

public class EndpointInfo
{
    [JsonPropertyName("id")]
    public string ID { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("host_display_name")]
    public string HostDisplayName { get; set; }
    [JsonPropertyName("monitored")]
    public bool Monitored { get; set; }
    [JsonPropertyName("monitor_heartbeat")]
    public bool MonitorHeartbeat { get; set; }
    [JsonPropertyName("heartbeat_information")]
    public HeartbeatInfo HeartbeatInfo { get; set; }
    [JsonPropertyName("is_sending_heartbeats")]
    public bool IsSendingHeartbeats { get; set; }
}

public class HeartbeatInfo
{
    [JsonPropertyName("last_report_at")]
    public DateTimeOffset LastReportAt { get; set; }
    [JsonPropertyName("reported_status")]
    public string Status { get; set; }
}

public class EndpointDetails
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("host_id")]
    public string HostID { get; set; }
    [JsonPropertyName("host")]
    public string Host { get; set; }
}