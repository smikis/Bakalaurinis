namespace TinkloProblemos.API.Contracts
{
    public class CategoryProblem
    {
        public int CategoryId { get; set; }
        public int ProblemId { get; set; }

        public Category.Category Category { get; set; }
        public Problem Problem { get; set; }
    }
}
