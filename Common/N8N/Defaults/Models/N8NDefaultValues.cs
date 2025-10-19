namespace Common.N8N.Defaults.Models;

public class N8NDefaultValues
{
    public N8NDefaultValues()
    {
    }

    public N8NDefaultValues(string companyNamePlaceholder, string companyServicesPlaceholder, string companyWebsitePlaceholder)
    {
        CompanyNamePlaceholder = companyNamePlaceholder;
        CompanyServicesPlaceholder = companyServicesPlaceholder;
        CompanyWebsitePlaceholder = companyWebsitePlaceholder;
    }

    public string? CompanyNamePlaceholder { get; set; } = "[COMPANY_NAME]";
    public string? CompanyServicesPlaceholder { get; set; } = "[COMPANY_SERVICES]";
    public string? CompanyWebsitePlaceholder { get; set; } = "[COMPANY_WEBSITE]";
    public string? DefaultWorkflowJson { get; set; }
    public string? DefaultSystemMessageJson { get; set; }
    public string? DefaultRulesJson { get; set; }
    public string? DefaultTemplateColumnTypeJson { get; set; }
    public string? DefaultTemplateColumnJson { get; set; }
}