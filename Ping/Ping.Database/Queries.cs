using System;
using System.Collections.Generic;
using System.Text;

namespace Ping.Database
{
    public static class Queries
    {
        public static string GetInternetUsers = @"SELECT id, ipAddress FROM internetuser;";

        public static string InsertInternetUser = @"INSERT INTO ping_results(internetUserId, ipAddress,time,recorded, status)
VALUES(@InternetUserId,@IpAddress,@Time,@Recorded, @Status);";
    }
}
