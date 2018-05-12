using System;
using System.Collections.Generic;
using System.Text;

namespace Ping.Database.Models
{
    public class PingResult
    {
        public int InternetUserId { get; set; }
        public string IpAddress { get; set; }
        public long Time { get; set; }
        public DateTime Recorded { get; set; }
        public string Status { get; set; }
    }
}
