using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Category;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface ICategoryService
    {
        bool Add(CategoryDto category);
        IEnumerable<CategoryDto> GetAll();
        CategoryDto GetById(int id);
        bool Delete(int id);
        bool Update(CategoryDto category);
    }
}