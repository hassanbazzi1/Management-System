using System.Text;
using Common.JSON;
using Common.Misc;
using Common.N8N.API.Models;
using Common.Web;
using Newtonsoft.Json.Linq;

namespace Common.N8N.API.Services;

public partial class N8NApiService
{
    public async Task<DataActionResult<WorkflowList>> GetAllWorkflows(bool? active, string[]? tags, string? name, string? projectId, bool? excludePinnedData, int? limit, string? cursor)
    {
        var result = new DataActionResult<WorkflowList>();
        try
        {
            var httpClient = _httpWebService.GetAuthorizedHttpClient(HttpConfig.N8NAPIHttpClient);
            var uri = UriUtil.GetUri(null, _apiOptions.WorkflowPath, new Dictionary<string, object?>
            {
                { "active", active },
                { "tags", tags },
                { "name", name },
                { "projectId", projectId },
                { "excludePinnedData", excludePinnedData },
                { "limit", limit },
                { "cursor", cursor }
            });

            var response = await httpClient.GetAsync(uri.ToString()); // http://192.168.0.1:915/api/Controller/Object

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                result.Data = JsonUtil.LoadJsonObjectFromString<WorkflowList>(res);
            }
            else
            {
                result.Message = response.StatusCode.ToString();
                throw new Exception(response.ReasonPhrase);
            }

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    public async Task<DataActionResult<Workflow>> GetWorkflow(string id, bool excludePinnedData)
    {
        var result = new DataActionResult<Workflow>();
        try
        {
            var httpClient = _httpWebService.GetAuthorizedHttpClient(HttpConfig.N8NAPIHttpClient);
            var uri = UriUtil.GetUri(null, _apiOptions.WorkflowPath + "/" + id, new Dictionary<string, object?>
            {
                { "excludePinnedData", excludePinnedData }
            });

            var response = await httpClient.GetAsync(uri.ToString()); // http://192.168.0.1:915/api/Controller/Object

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                result.Data = JsonUtil.LoadJsonObjectFromString<Workflow>(res);
            }
            else
            {
                result.Message = response.StatusCode.ToString();
                throw new Exception(response.ReasonPhrase);
            }

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    public async Task<DataActionResult<Workflow>> CreateWorkflow(string name, JArray nodeData, JObject connectionsData, JObject settingsData, JObject? staticData)
    {
        var result = new DataActionResult<Workflow>();
        try
        {
            var httpClient = _httpWebService.GetAuthorizedHttpClient(HttpConfig.N8NAPIHttpClient);
            var uri = UriUtil.GetUri(null, _apiOptions.WorkflowPath).ToString();
            var requestBody = JObject.FromObject(new
            {
                name,
                nodes = nodeData,
                connections = connectionsData,
                settings = settingsData,
                staticData = staticData == null ? "null" : staticData.ToString()
            });

            var content = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, content); // http://192.168.0.1:915/api/Controller/Object

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                result.Data = JsonUtil.LoadJsonObjectFromString<Workflow>(res);
            }
            else
            {
                result.Message = response.StatusCode.ToString();
                throw new Exception(response.ReasonPhrase);
            }

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    public async Task<DataActionResult<Workflow>> DeleteWorkflow(string id)
    {
        var result = new DataActionResult<Workflow>();
        try
        {
            var httpClient = _httpWebService.GetAuthorizedHttpClient(HttpConfig.N8NAPIHttpClient);
            var uri = UriUtil.GetUri(null, _apiOptions.WorkflowPath + "/" + id, null);

            var response = await httpClient.DeleteAsync(uri.ToString()); // http://192.168.0.1:915/api/Controller/Object

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                result.Data = JsonUtil.LoadJsonObjectFromString<Workflow>(res);
            }
            else
            {
                result.Message = response.StatusCode.ToString();
                throw new Exception(response.ReasonPhrase);
            }

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    public async Task<DataActionResult<Workflow>> UpdateWorkflow(Workflow workflow)
    {
        var result = new DataActionResult<Workflow>();
        try
        {
            var httpClient = _httpWebService.GetAuthorizedHttpClient(HttpConfig.N8NAPIHttpClient);
            var uri = UriUtil.GetUri(null, _apiOptions.WorkflowPath + "/" + workflow.Id).ToString();
            var requestBody = JObject.FromObject(new
            {
                name = workflow.Name,
                nodes = workflow.NodeData,
                connections = workflow.ConnectionsData,
                settings = workflow.SettingsData,
                staticData = workflow.StaticData == null ? "null" : workflow.StaticData.ToString()
            });

            var content = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(uri, content); // http://192.168.0.1:915/api/Controller/Object

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                result.Data = JsonUtil.LoadJsonObjectFromString<Workflow>(res);
            }
            else
            {
                result.Message = response.StatusCode.ToString();
                throw new Exception(response.ReasonPhrase);
            }

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }


    public async Task<DataActionResult<Workflow>> UpdateWorkflow(string id, string name, JArray nodeData, JObject connectionsData, JObject settingsData, JObject? staticData)
    {
        var result = new DataActionResult<Workflow>();
        try
        {
            var httpClient = _httpWebService.GetAuthorizedHttpClient(HttpConfig.N8NAPIHttpClient);
            var uri = UriUtil.GetUri(null, _apiOptions.WorkflowPath + "/" + id).ToString();
            var requestBody = JObject.FromObject(new
            {
                name,
                nodes = nodeData,
                connections = connectionsData,
                settings = settingsData,
                staticData = staticData == null ? "null" : staticData.ToString()
            });

            var content = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(uri, content); // http://192.168.0.1:915/api/Controller/Object

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                result.Data = JsonUtil.LoadJsonObjectFromString<Workflow>(res);
            }
            else
            {
                result.Message = response.StatusCode.ToString();
                throw new Exception(response.ReasonPhrase);
            }

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }
}