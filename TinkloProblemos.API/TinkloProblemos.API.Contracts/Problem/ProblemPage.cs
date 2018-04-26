using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Problem
{
    public class ProblemPage
    {
        public int Total { get; set; }
        public IEnumerable<GetProblem> Data { get; set; }
    }
}
