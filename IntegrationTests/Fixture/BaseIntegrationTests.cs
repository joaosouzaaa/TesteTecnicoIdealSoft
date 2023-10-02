namespace IntegrationTests.Fixture;
public abstract class BaseIntegrationTests<TEntity> : IClassFixture<HttpClientFactory> 
    where TEntity : class
{
    private readonly HttpClientFactory _httpClientFactory;
    protected readonly HttpClient _httpClient;

    public BaseIntegrationTests(HttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient();
    }
}
