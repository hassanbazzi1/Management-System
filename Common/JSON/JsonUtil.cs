using Common.File;
using Newtonsoft.Json;

namespace Common.JSON;

public class JsonUtil
{
    public static T LoadJsonObjectFromFile<T>(string file)
    {
        var json = FileUtil.ReadAllText(file);
        return LoadJsonObjectFromString<T>(json);
    }

    public static T LoadJsonObjectFromString<T>(string json)
    {
        var jsonObject = JsonConvert.DeserializeObject<T>(json);
        return jsonObject;
    }
}