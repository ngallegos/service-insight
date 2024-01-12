namespace ServiceInsight.Web.Model;

public class ServiceControlConfiguration
{
    public List<ServiceControlEnvironment> Environments { get; set; } = new List<ServiceControlEnvironment>();
}

public class ServiceControlEnvironment
{
    public string Name { get; set; }
    public string ApiUrl { get; set; }
}