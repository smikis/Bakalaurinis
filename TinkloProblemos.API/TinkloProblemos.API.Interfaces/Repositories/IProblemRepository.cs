using TinkloProblemos.API.Contracts.Problem;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface IProblemRepository
    {
        int Add(CreateProblem problem);
    }
}