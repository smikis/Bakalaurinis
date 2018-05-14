using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Reports
{
    public class UserReport
    {
        public int TimeSpentCount { get; set; }
        public int ProblemsSolved { get; set; }
        public double Time { get; set; }
        public double MaxTime { get; set; }
        public double MinTime { get; set; }
        public double AverageTaskTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
    }
}
