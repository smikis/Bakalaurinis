using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Ping
{
    public class GetPing
    {
        public int InternetUserId { get; set; }
        public string IpAddress { get; set; }
        public long Time { get; set; }
        public DateTime Recorded { get; set; }
        public string Status { get; set; }
    }
}
