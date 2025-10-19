using Common.Auth;
using Common.Auth.JWT;

namespace Common.Web.HttpService;

public class JwtHttpWebService : HttpWebService
{
    protected ApiCredentials? _apiCredentials;
    protected JwtService _jwtService;

    protected JwtHttpWebService(IHttpClientFactory httpClientFactory, JwtService jwtService) : base(httpClientFactory)
    {
        _jwtService = jwtService;
    }

    protected JwtHttpWebService(IHttpClientFactory httpClientFactory, JwtService jwtService, ApiCredentials? apiCredentials) : base(httpClientFactory)
    {
        _jwtService = jwtService;
        _apiCredentials = apiCredentials;
    }

    public HttpClient GetAuthorizedHttpClient(string name, JwtSigningConfig signingConfig, Dictionary<string, string> claims, TimeSpan? expiry = null)
    {
        var httpClient = GetAuthorizedHttpClient(name);
        var jwtToken = _jwtService.Encode(signingConfig, claims, expiry);
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        if (_apiCredentials != null)
            httpClient.DefaultRequestHeaders.Add(_apiCredentials.KeyName, _apiCredentials.ApiKey);

        return httpClient;
    }
}