using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Comment;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        int Add(CreateComment prod);
        IEnumerable<GetComment> GetAll();
        IEnumerable<GetComment> GetAll(int problemId);
    }
}