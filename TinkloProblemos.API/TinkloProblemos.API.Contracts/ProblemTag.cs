namespace TinkloProblemos.API.Contracts
{
    public class ProblemTag
    {
        public int TagId { get; set; }
        public int ProblemId { get; set; }

        public Problem.Problem Problem { get; set; }
        public Tag Tag { get; set; }
    }
}
