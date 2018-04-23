using TinkloProblemos.API.Identity.Entities;

namespace TinkloProblemos.API.Contracts
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ProblemId { get; set; }

        public Problem.Problem Problem { get; set; }
        public ApplicationUser User { get; set; }
    }
}
