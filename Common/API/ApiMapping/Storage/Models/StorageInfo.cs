namespace Common.API.ApiMapping.Storage.Models
{
    public class StorageInfo
    {
        public long UsedBytes { get; init; }
        public long TotalBytes { get; init; }
        public long FreeBytes { get; init; }

        public string Used => Format(UsedBytes);
        public string Total => Format(TotalBytes);
        public string Free => Format(FreeBytes);

        private static string Format(long b)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB" };
            int i = 0;
            double d = b;
            while (d >= 1024 && i < suf.Length-1)
            {
                d /= 1024;
                i++;
            }
            return $"{d:0.##} {suf[i]}";
        }
    }
}
