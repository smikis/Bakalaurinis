using System;
using System.Collections.Generic;
using System.Data;
using TinkloProblemos.API.Contracts.Category;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public bool Add(CategoryDto category)
        {
            if (_categoryRepository.Add(category) != 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public CategoryDto GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public bool Delete(int id)
        {
            if (_categoryRepository.Delete(id) != 0)
            {
                return true;
            }
            return false;
        }

        public bool Update(CategoryDto category)
        {
            if (_categoryRepository.Update(category) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
