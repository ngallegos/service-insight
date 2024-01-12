using System.Web;
using Microsoft.Extensions.Options;
using ServiceInsight.Web.Model;

namespace ServiceInsight.Web.Services;

public interface IServiceControlClient
{
    Task<List<EndpointInfo>> GetMonitoredEndpoints();
    Task<List<MessageInfo>> SearchMessages(string keyword = null, string endpoint = null,
        int page = 1, int perPage = 25,
        string direction = "desc",
        string sort = "time_sent");

    Task<string> GetMessageBody(string bodyUrl);
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
    
    public async Task<List<MessageInfo>> SearchMessages(string keyword = null, string endpoint = null, 
        int page = 1, int perPage = 25,
        string direction = "desc",
        string sort = "time_sent")
    {
        keyword = HttpUtility.UrlEncode(keyword);
        var uri = "messages";
        if (!string.IsNullOrEmpty(keyword))
            uri += $"/search/{keyword}";
        uri += $"?page={page}&per_page={perPage}&direction={direction}&sort={sort}";
        if (!string.IsNullOrEmpty(endpoint))
            uri = $"endpoints/{endpoint}/" + uri;
        var messages = await _client.GetFromJsonAsync<List<MessageInfo>>(uri);

        return messages;
    }

    public async Task<string> GetMessageBody(string bodyUrl)
    {
        return await _client.GetStringAsync(bodyUrl.TrimStart('/'));
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
