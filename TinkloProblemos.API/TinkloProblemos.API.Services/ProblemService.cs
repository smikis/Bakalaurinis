using System;
using System.Collections.Generic;
using System.Text;
using TinkloProblemos.API.Contracts.Problem;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        public ProblemService(IProblemRepository problemRepository)
        {
            _problemRepository = problemRepository;
        }
        public bool Add(CreateProblem createProblem)
        {
            if (_problemRepository.Add(createProblem) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
