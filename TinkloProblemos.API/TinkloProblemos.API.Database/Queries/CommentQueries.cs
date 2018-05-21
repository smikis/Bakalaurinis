namespace TinkloProblemos.API.Database.Queries
{
    public static class CommentQueries
    {
        public static string Add = @"INSERT INTO comment (Text, UserId, ProblemId) 
                                    VALUES(@Text, @UserId, @ProblemId)";
        public static string GetAll = @"SELECT comment.Id, comment.Text, users.firstName, users.lastName FROM comment inner join users on comment.userId = users.id;";

        public static string GetAllProblemComments = @"SELECT comment.Id, comment.Text, users.firstName, users.lastName FROM comment inner join users on comment.userId = users.id
                                                WHERE comment.problemId = @problemId;";

        public static string GetById = @"SELECT comment.Text, users.firstName, users.lastName FROM comment inner join users on comment.userId = users.id
                                         WHERE Id = @Id";
        public static string Update = @"UPDATE comment SET Text = @Text
                                   WHERE Id = @Id";
        public static string Delete = @"DELETE FROM comment
                                 WHERE Id = @Id";

    }
}
