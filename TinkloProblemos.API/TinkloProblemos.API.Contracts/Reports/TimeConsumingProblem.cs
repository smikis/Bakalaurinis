using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Reports
{
    public class TimeConsumingProblem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TimeSpent { get; set; }
    }
}
