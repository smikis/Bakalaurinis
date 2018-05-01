using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Comment;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface ICommentService
    {
        bool Add(CreateComment comment);
        IEnumerable<GetComment> GetAll();
        IEnumerable<GetComment> GetAll(int problemId);
    }
}