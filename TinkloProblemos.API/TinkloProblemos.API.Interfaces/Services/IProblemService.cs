using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Models;
using TinkloProblemos.API.Contracts.Problem;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IProblemService
    {
        DatabaseResult Add(CreateProblem createProblem);
    }
}