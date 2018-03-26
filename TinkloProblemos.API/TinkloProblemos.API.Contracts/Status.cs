using System.Collections.Generic;

namespace TinkloProblemos.API.Contracts
{
    public class Status
    {
        public Status()
        {
            Problem = new HashSet<Problem>();
        }

        public int Id { get; set; }
        public int Name { get; set; }

        public ICollection<Problem> Problem { get; set; }
    }
}
