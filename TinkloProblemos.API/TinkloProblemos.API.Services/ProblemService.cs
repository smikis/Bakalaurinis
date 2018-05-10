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
        private readonly ITagService _tagRepository;
        public ProblemService(IProblemRepository problemRepository, ITagService tagRepository)
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
                    var tags = createProblem.Tags.Select(x => new CreateTagDto {Name = x});
                    foreach (var createTagDto in tags)
                    {
                        _tagRepository.AddProblemTag(createTagDto, result);
                    }
                }
            }

            return databaseResult;
        }

        public GetProblem GetProblem(int id)
        {
            return _problemRepository.GetProblem(id);
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
                var sqlSearchQuery = SqlQueryHelper.ConvertToSqlSearchQuery(searchTerm);
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

       
    }
}
