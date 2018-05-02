using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.TimeSpent
{
    public class UpdateTimeSpent
    {
        public string Description { get; set; }
        public decimal HoursSpent { get; set; }
    }
}
