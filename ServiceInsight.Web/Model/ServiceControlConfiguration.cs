namespace ServiceInsight.Web.Model;

public class ServiceControlConfiguration
{
    public List<ServiceControlInstance> Instances { get; set; } = new List<ServiceControlInstance>();
}

public class ServiceControlInstance
{
    public string Name { get; set; }
    public string ApiUrl { get; set; }
}