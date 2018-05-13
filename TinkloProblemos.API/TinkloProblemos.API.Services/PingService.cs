using System;
using System.Collections.Generic;
using System.Text;
using TinkloProblemos.API.Contracts.Ping;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class PingService : IPingService
    {
        private readonly IPingRepository _pingRepository;
        public PingService(IPingRepository pingRepository)
        {
            _pingRepository = pingRepository;
        }

        public IEnumerable<GetPing> GetLatestInternetUserPings(int internetUserId)
        {
            return _pingRepository.GetInternetUserPingInformation(internetUserId, 10);
        }

        public PingInformation GetInternetUserPingInformation(int internetUserId)
        {
            var lastFailed = _pingRepository.GetLastFailedInternetUserPing(internetUserId);
            if (lastFailed != null)
            {
                var lastSuccessfull = _pingRepository.GetLastSuccessfullInternetUserPing(internetUserId, lastFailed.Recorded);
                if (lastSuccessfull != null && lastSuccessfull.Recorded > lastFailed.Recorded)
                {
                    TimeSpan duration = DateTime.Now.Subtract(lastSuccessfull.Recorded);
                    return new PingInformation
                    {
                        LastFailDate = lastFailed.Recorded,
                        Uptime = (long)duration.TotalMilliseconds
                    };
                }

                return new PingInformation
                {
                    LastFailDate = lastFailed.Recorded,
                    Uptime = 0
                };
            }

            var firstSuccessfull = _pingRepository.GetFirstSuccessfullInternetUserPing(internetUserId);
            TimeSpan durationFirst = DateTime.Now.Subtract(firstSuccessfull.Recorded);
            return new PingInformation
            {
                LastFailDate = null,
                Uptime = (long)durationFirst.TotalMilliseconds
            };
        }
    }
}
