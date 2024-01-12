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
    Task<List<MessageInfo>> GetConversation(string conversationID);
    Task<SagaDetail> GetSaga(string sagaID);
}

public class ServiceControlClient : IServiceControlClient
{
    private readonly HttpClient _client;
    public string Environment { get; }

    public ServiceControlClient(HttpClient client, string environment)
    {
        _client = client;
        Environment = environment;
    }

    public async Task<List<EndpointInfo>> GetMonitoredEndpoints()
    {
        var endpoints = (await _client.GetFromJsonAsync<List<EndpointInfo>>("endpoints"))
            .Where(x => x.Monitored).ToList();

        endpoints.ForEach(e =>
        {
            e.Environment = Environment;
        });
        
        return endpoints;
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

        messages.ForEach(m =>
        {
            m.Environment = Environment;
        });
        
        return messages;
    }

    public async Task<string> GetMessageBody(string bodyUrl)
    {
        return await _client.GetStringAsync(bodyUrl.TrimStart('/'));
    }
    
    public async Task<List<MessageInfo>> GetConversation(string conversationID)
    {
        var messages = await _client.GetFromJsonAsync<List<MessageInfo>>($"conversations/{conversationID}");
        return messages.OrderBy(x => x.TimeSent).ToList();
    }

    public async Task<SagaDetail> GetSaga(string sagaID)
    {
        var saga = await _client.GetFromJsonAsync<SagaDetail>($"sagas/{sagaID}");
        return saga;
    }
}

public interface IServiceControlClientFactory
{
    IServiceControlClient GetClient(string environment);
    List<IServiceControlClient> GetAllClients();
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

    public IServiceControlClient GetClient(string environment)
    {
        var endpoint = GetInstance(environment);

        var client = _httpClientFactory.CreateClient($"service-control-{environment}");
        client.BaseAddress = new Uri(endpoint.ApiUrl);

        return new ServiceControlClient(client, environment);
    }

    public List<IServiceControlClient> GetAllClients()
    {
        return _configuration.Environments.Select(x => x.Name).Select(GetClient).ToList();
    }

    private ServiceControlEnvironment GetInstance(string endpointName)
    {
        return _configuration.Environments.FirstOrDefault(x => string.Equals(x.Name, endpointName, StringComparison.CurrentCultureIgnoreCase));
    }
}
