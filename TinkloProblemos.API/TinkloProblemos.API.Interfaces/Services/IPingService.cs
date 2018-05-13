using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Ping;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IPingService
    {
        PingInformation GetInternetUserPingInformation(int internetUserId);
        IEnumerable<GetPing> GetLatestInternetUserPings(int internetUserId);
    }
}