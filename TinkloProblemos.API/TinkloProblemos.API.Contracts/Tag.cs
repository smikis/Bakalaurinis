using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Database
{
    public partial class Tag
    {
        public Tag()
        {
            ProblemTag = new HashSet<ProblemTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProblemTag> ProblemTag { get; set; }
    }
}
