using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Comment;
using TinkloProblemos.API.Contracts.InternetUser;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public bool Add(CreateComment comment)
        {
            if (_commentRepository.Add(comment) != 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<GetComment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public IEnumerable<GetComment> GetAll(int problemId)
        {
            return _commentRepository.GetAll(problemId);
        }

    }
}
