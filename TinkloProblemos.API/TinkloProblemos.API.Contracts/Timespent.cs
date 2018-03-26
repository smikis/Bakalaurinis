using System;

namespace TinkloProblemos.API.Contracts
{
    public class Timespent
    {
        public int Id { get; set; }
        public decimal HoursSpent { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateRecorded { get; set; }
        public int UserId { get; set; }
        public int ProblemId { get; set; }

        public Problem Problem { get; set; }
        public User User { get; set; }
    }
}
