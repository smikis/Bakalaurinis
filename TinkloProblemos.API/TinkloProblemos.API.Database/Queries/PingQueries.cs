namespace TinkloProblemos.API.Database.Queries
{
    public static class PingQueries
    {
        public static string GetInternetUserPingResults = @"SELECT * FROM bakalaurinis.ping_results
WHERE internetUserId = @internetUserId
ORDER BY id desc
LIMIT @limit;";
        public static string GetLastFailedPing = @"SELECT * FROM bakalaurinis.ping_results
WHERE internetUserId = @internetUserId && status != 'Success'
ORDER BY id desc
LIMIT 1;";
        public static string GetLastSuccessfullPing = @"SELECT * FROM bakalaurinis.ping_results
WHERE internetUserId = @internetUserId && status = 'Success'
&& recorded > @dateFrom
LIMIT 1;";
        public static string GetFirstSuccessfullPing = @"SELECT * FROM bakalaurinis.ping_results
WHERE internetUserId = @internetUserId && status = 'Success'
LIMIT 1;";

    }
}
