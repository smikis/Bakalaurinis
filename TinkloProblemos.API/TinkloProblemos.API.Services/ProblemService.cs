using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinkloProblemos.API.Contracts.Models;
using TinkloProblemos.API.Contracts.Problem;
using TinkloProblemos.API.Contracts.Tag;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly ITagRepository _tagRepository;
        public ProblemService(IProblemRepository problemRepository, ITagRepository tagRepository)
        {
            _problemRepository = problemRepository;
            _tagRepository = tagRepository;
        }
        public DatabaseResult Add(CreateProblem createProblem)
        {
            var databaseResult = new DatabaseResult
            {
                Success = false
            };

            var result = _problemRepository.Add(createProblem);

            if (result != 0)
            {
                databaseResult.Key = result;
                databaseResult.Success = true;
                if (createProblem.Tags != null && createProblem.Tags.Any())
                {
                    var problemTags = createProblem.Tags.Select(x => new ProblemTagDto { ProblemId = result, TagId = x });
                    _tagRepository.AddToProblem(problemTags);
                }
            }

            return databaseResult;
        }

        public IEnumerable<GetProblem> GetProblems(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return _problemRepository.GetProblems(skip, pageSize);
        }

        public ProblemPage GetProblems(int page, int pageSize, string category, string status, string assingnedUser, string searchTerm, DateTime? dateFrom, DateTime? dateTo)
        {
            int skip = page * pageSize;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var sqlSearchQuery = ConvertToSqlSearchQuery(searchTerm);
                return _problemRepository.GetProblemsFilteredSearch(skip, pageSize, category, status, assingnedUser, sqlSearchQuery, dateFrom, dateTo);
            }
            return _problemRepository.GetProblemsFiltered(skip, pageSize, category, status, assingnedUser, dateFrom, dateTo);
        }

        public IEnumerable<GetProblem> GetProblems(string category, string status, string assingnedUser, DateTime? dateFrom, DateTime? dateTo)
        {
            return _problemRepository.GetProblemsFiltered(category, status, assingnedUser, dateFrom, dateTo);
        }

        public IEnumerable<GetProblem> GetUserProblems(string category, string status, string assingnedUser)
        {
            return _problemRepository.GetProblemsUser(category, status, assingnedUser);
        }

        private string ConvertToSqlSearchQuery(string searchTerm)
        {
            var words = searchTerm.Split(' ');
            if (words.Length == 1)
            {
                return $"+{searchTerm}*";
            }

            var searchQuery = new StringBuilder();
            foreach (var word in words)
            {
                searchQuery.Append($"+{word}* ");
            }

            return searchQuery.ToString();
        }
    }
}
