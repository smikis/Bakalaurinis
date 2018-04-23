using System;
using System.Collections.Generic;
using System.Text;
using TinkloProblemos.API.Contracts.Tag;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<TagDto> GetAll()
        {
            return _tagRepository.GetAll();
        }

        public bool Add(CreateTagDto tag)
        {
            if (_tagRepository.Add(tag) != 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<TagDto> GetProblemTags(int problemId)
        {
            return _tagRepository.GetProblemTags(problemId);
        }

        public bool AddToProblem(int tagId, int problemId)
        {
            var problemTag = new ProblemTagDto
            {
                ProblemId = problemId,
                TagId = tagId
            };
            if (_tagRepository.AddToProblem(problemTag) != 0)
            {
                return true;
            }
            return false;
        }

        public bool AddToProblem(ProblemTagDto problemTag)
        {          
            if (_tagRepository.AddToProblem(problemTag) != 0)
            {
                return true;
            }
            return false;
        }

        public bool RemoveFromProblem(int tagId, int problemId)
        {
            var problemTag = new ProblemTagDto
            {
                ProblemId = problemId,
                TagId = tagId
            };
            if (_tagRepository.RemoveFromProblem(problemTag) != 0)
            {
                return true;
            }
            return false;
        }

        public bool RemoveFromProblem(ProblemTagDto problemTag)
        {
            if (_tagRepository.RemoveFromProblem(problemTag) != 0)
            {
                return true;
            }
            return false;
        }
        public bool Delete(int id)
        {
            if (_tagRepository.Delete(id) != 0)
            {
                return true;
            }
            return false;
        }

    }
}
