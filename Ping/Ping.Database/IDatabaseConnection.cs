using System.Collections.Generic;
using Ping.Database.Models;

namespace Ping.Database
{
    public interface IDatabaseConnection
    {
        IEnumerable<InternetUser> GetInternetUsers();
        void InsertPingInformation(List<PingResult> results);
    }
}