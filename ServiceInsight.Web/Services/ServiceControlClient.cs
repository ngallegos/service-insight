using Microsoft.Extensions.Options;
using ServiceInsight.Web.Model;

namespace ServiceInsight.Web.Services;

public interface IServiceControlClient
{
    Task<List<EndpointInfo>> GetMonitoredEndpoints();
}

public class ServiceControlClient : IServiceControlClient
{
    private readonly HttpClient _client;

    public ServiceControlClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<EndpointInfo>> GetMonitoredEndpoints()
    {
        var endpoints = await _client.GetFromJsonAsync<List<EndpointInfo>>("endpoints");
        return endpoints.Where(x => x.Monitored).ToList();
    }
}

public interface IServiceControlClientFactory
{
    IServiceControlClient GetClient(string endpointName);
}

public class ServiceControlClientFactory : IServiceControlClientFactory
{
    private readonly ServiceControlConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public ServiceControlClientFactory(IOptions<ServiceControlConfiguration> options,
        IHttpClientFactory httpClientFactory)
    {
        _configuration = options.Value;
        _httpClientFactory = httpClientFactory;
    }

    public IServiceControlClient GetClient(string endpointName)
    {
        var endpoint = GetInstance(endpointName);

        var client = _httpClientFactory.CreateClient($"service-control-{endpointName}");
        client.BaseAddress = new Uri(endpoint.ApiUrl);

        return new ServiceControlClient(client);
    }

    private ServiceControlInstance GetInstance(string endpointName)
    {
        return _configuration.Instances.FirstOrDefault(x => string.Equals(x.Name, endpointName, StringComparison.CurrentCultureIgnoreCase));
    }
}
