using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.TimeSpent
{
    public class GetTimeSpentCalendar
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public decimal HoursSpent { get; set; }
        public string Description { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
