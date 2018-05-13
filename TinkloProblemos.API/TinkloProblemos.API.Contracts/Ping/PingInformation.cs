using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Ping
{
    public class PingInformation
    {
        public DateTime? LastFailDate { get; set; }
        public long Uptime { get; set; }
    }
}
