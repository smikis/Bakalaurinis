using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Category;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        int Add(CategoryDto prod);
        IEnumerable<CategoryDto> GetAll();
        CategoryDto GetById(int id);
        int Delete(int id);
        int Update(CategoryUpdateDto prod, int categoryId);
    }
}