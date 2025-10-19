namespace Common.API.ApiMapping.Sessions.Models
{
    public class MonthlySessionsResponse
    {
        public int[] current { get; set; } = new int[12];

        public int[] previous { get; set; } = new int[12];
    }
}
