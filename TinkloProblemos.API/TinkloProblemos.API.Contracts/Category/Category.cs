using System.Collections.Generic;

namespace TinkloProblemos.API.Contracts.Category
{
    public class Category
    {
        public Category()
        {
            CategoryProblem = new HashSet<CategoryProblem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CategoryProblem> CategoryProblem { get; set; }
    }
}
