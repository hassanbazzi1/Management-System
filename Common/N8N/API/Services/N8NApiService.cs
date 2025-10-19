using Common.N8N.API.Models;
using Common.Web.HttpService;
using Microsoft.Extensions.Options;

namespace Common.N8N.API.Services;

public partial class N8NApiService
{
    protected N8NApiOptions _apiOptions;
    protected HttpWebService _httpWebService;

    public N8NApiService(HttpWebService httpWebService, IOptions<N8NApiOptions> apiOptions)
    {
        _httpWebService = httpWebService;
        _apiOptions = apiOptions.Value;
    }
}