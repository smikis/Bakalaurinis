using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Reports
{
    public class TimeConsumingInternetUsers
    {
        public int InternetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double TimeSpent { get; set; }
    }
}
