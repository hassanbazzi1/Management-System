namespace Common.N8N.API.Models;

public class N8NApiOptions
{
    public N8NApiOptions(string workflowPath, string variablePath, string userPath, string tagPath, string projectPath, string executionPath, string credentialPath)
    {
        WorkflowPath = workflowPath;
        VariablePath = variablePath;
        UserPath = userPath;
        TagPath = tagPath;
        ProjectPath = projectPath;
        ExecutionPath = executionPath;
        CredentialPath = credentialPath;
    }

    public string WorkflowPath { get; set; }
    public string CredentialPath { get; set; }
    public string ExecutionPath { get; set; }
    public string ProjectPath { get; set; }
    public string TagPath { get; set; }
    public string UserPath { get; set; }
    public string VariablePath { get; set; }
}