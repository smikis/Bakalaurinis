namespace TinkloProblemos.API.Database.Queries
{
    public static class ProblemCategoryQueries
    {
        public static string Add = @"INSERT INTO category_problem (CategoryId,ProblemId) 
                                    VALUES(@CategoryId, @ProblemId)";

    }
}
