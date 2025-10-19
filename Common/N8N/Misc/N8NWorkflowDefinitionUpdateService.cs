using Common.DB.ClientDB.Models;
using Common.Misc;
using Newtonsoft.Json.Linq;

namespace Common.N8N.Misc;

public class N8NWorkflowDefinitionUpdateService
{
    private const string N8NAiAgentNodeType = "@n8n/n8n-nodes-langchain.agent";


    public DataActionResult<string> UpdateCredentials(string workflowJson, TwilioCredential twilioCredential, OpenAiCredential openAiCredential)
    {
        var result = new DataActionResult<string>();
        try
        {
            // TODO: Handle mutliple workflows (e.g. subworkflow). The AI agent workflow must be passed and possibly defined by a type in the database
            var json = JObject.Parse(workflowJson);
            //var agent = json.SelectToken("nodes").Where(x=>(string)x["type"] == N8N_AI_AGENT_NODE_TYPE)["parameters"]["options"]["systemMessage"] = sys

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }

    // TODO: Should rules be pulled directly from database by ai agent or be part of system messaage
    public DataActionResult<string> UpdateAiAgent(string workflowJson, string systemMessageJson, string? rulesJson)
    {
        var result = new DataActionResult<string>();
        try
        {
            var json = JObject.Parse(workflowJson);
            json.SelectToken("nodes").First(x => (string)x["type"] == N8NAiAgentNodeType)["parameters.options.systemMessage"] = systemMessageJson;
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }

    public DataActionResult<string> UpdatePhoneNumber(string workflowJson, string phoneNumber)
    {
        var result = new DataActionResult<string>();
        try
        {
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }
}