namespace Common.Web.HttpService;

public class HttpWebService
{
    protected IHttpClientFactory _httpClientFactory;

    protected HttpWebService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public HttpClient GetAuthorizedHttpClient(string name)
    {
        return _httpClientFactory.CreateClient(name);
    }
}