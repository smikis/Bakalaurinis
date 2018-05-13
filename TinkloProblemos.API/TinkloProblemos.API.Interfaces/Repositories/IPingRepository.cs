using System;
using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Ping;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface IPingRepository
    {
        IEnumerable<GetPing> GetInternetUserPingInformation(int internetUserId, int limit);
        GetPing GetLastSuccessfullInternetUserPing(int internetUserId, DateTime dateFrom);
        GetPing GetLastFailedInternetUserPing(int internetUserId);
        GetPing GetFirstSuccessfullInternetUserPing(int internetUserId);
    }
}