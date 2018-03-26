using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Database
{
    public partial class CategoryProblem
    {
        public int CategoryId { get; set; }
        public int ProblemId { get; set; }

        public Category Category { get; set; }
        public Problem Problem { get; set; }
    }
}
