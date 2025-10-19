using Common.AI.Models;
using Common.Misc;
using OpenAI.Embeddings;

namespace Common.AI.OpenAI;

public class OpenAiService : AiService
{
    public const string OpenaiApiGetEmbeddingsPath = "embeddings";
    private readonly OpenAiSettings _settings;

    public OpenAiService(OpenAiSettings settings)
    {
        _settings = settings;
    }

    public async Task<DataActionResult<List<Embedding>>> GetEmbeddings(string[] text)
    {
        var result = new DataActionResult<List<Embedding>>();
        try
        {
            EmbeddingClient client = new(_settings.EmbeddingsModel, _settings.ApiKey);
            EmbeddingGenerationOptions options = new() { Dimensions = _settings.EmbeddingsDimension };
            var embedding = await client.GenerateEmbeddingsAsync(text, options);
            result.Data = new List<Embedding>();
            for (var i = 0; i < embedding.Value.Count; i++)
                result.Data.Add(new Embedding(embedding.Value.ElementAt(i).ToFloats().ToArray()));

            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception ex)
        {
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = ex;
        }

        return result;
    }
}