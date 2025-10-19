namespace Common.File;

public class FileUtil
{
    public static string ReadAllText(string filePath)
    {
        using (var r = new StreamReader(filePath))
        {
            var str = r.ReadToEnd();
            return str;
        }
    }
}