using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Tag;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface ITagService
    {
        bool Add(CreateTagDto tag);
        IEnumerable<TagDto> GetProblemTags(int problemId);
        bool AddToProblem(int tagId, int problemId);
        bool AddToProblem(ProblemTagDto problemTag);
        bool RemoveFromProblem(int tagId, int problemId);
        bool RemoveFromProblem(ProblemTagDto problemTag);
        bool Delete(int id);
        IEnumerable<TagDto> GetAll();
        TagDto AddProblemTag(CreateTagDto tag, int problemId);
    }
}