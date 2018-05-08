using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.TimeSpent
{
    public class GetTimeSpent
    {
        public int Id { get; set; }
        public decimal HoursSpent { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int ProblemId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
