using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Database
{
    public partial class Status
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
