using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Tag;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface ITagRepository
    {
        int Add(CreateTagDto prod);
        IEnumerable<TagDto> GetAll();
        IEnumerable<TagDto> GetProblemTags(int problemId);
        int AddToProblem(ProblemTagDto problemTag);
        int AddToProblem(IEnumerable<ProblemTagDto> problemTags);
        int RemoveFromProblem(ProblemTagDto problemTag);
        int Delete(int id);
    }
}