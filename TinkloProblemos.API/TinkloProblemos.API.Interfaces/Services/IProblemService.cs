using TinkloProblemos.API.Contracts.Problem;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IProblemService
    {
        bool Add(CreateProblem createProblem);
    }
}