using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.TimeSpent
{
    public class CreateTimeSpent
    {
        public decimal HoursSpent { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int ProblemId { get; set; }
    }
}
