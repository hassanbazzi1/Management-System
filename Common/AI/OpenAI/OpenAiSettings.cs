namespace Common.AI.OpenAI;

public class OpenAiSettings
{
    public OpenAiSettings(string apiUrl, string apiKey, string organizationId, string embeddingsModel, int embeddingsDimension)
    {
        ApiUrl = apiUrl;
        ApiKey = apiKey;
        OrganizationId = organizationId;
        EmbeddingsModel = embeddingsModel;
        EmbeddingsDimension = embeddingsDimension;
    }

    public string ApiUrl { get; set; }
    public string ApiKey { get; set; }
    public string OrganizationId { get; set; }
    public string EmbeddingsModel { get; set; }
    public int EmbeddingsDimension { get; set; }
}