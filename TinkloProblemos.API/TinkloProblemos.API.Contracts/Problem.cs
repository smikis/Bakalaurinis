using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Contracts
{
    public class Problem
    {
        public Problem()
        {
            CategoryProblem = new HashSet<CategoryProblem>();
            Comment = new HashSet<Comment>();
            ProblemTag = new HashSet<ProblemTag>();
            Timespent = new HashSet<Timespent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int AssignedUser { get; set; }
        public int StatusId { get; set; }
        public int InternetUserId { get; set; }
        public DateTime Created { get; set; }

        public User AssignedUserNavigation { get; set; }
        public Internetuser InternetUser { get; set; }
        public Status Status { get; set; }
        public ICollection<CategoryProblem> CategoryProblem { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<ProblemTag> ProblemTag { get; set; }
        public ICollection<Timespent> Timespent { get; set; }
    }
}
