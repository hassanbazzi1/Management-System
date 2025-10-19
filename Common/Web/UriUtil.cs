using System.Web;

namespace Common.Web;

public class UriUtil
{
    public static Uri GetUri(string basePath, string path)
    {
        var uriBuilder = new UriBuilder(basePath + "/" + path);
        return uriBuilder.Uri;
    }

    public static Uri GetUri(string? basePath, string path, Dictionary<string, object?>? parameters)
    {
        var uriBuilder = basePath != null ? new UriBuilder(basePath + path) : new UriBuilder(path);

        if (parameters != null && parameters.Count > 0)
        {
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var item in parameters)
                if (item.Value != null)
                {
                    if (item.Value.GetType() != typeof(bool))
                        query[item.Key] = item.Value.ToString();
                    else
                        query[item.Key] = item.Value.ToString().ToLower();
                }

            uriBuilder.Query = query.ToString();
        }

        return uriBuilder.Uri;
    }
}