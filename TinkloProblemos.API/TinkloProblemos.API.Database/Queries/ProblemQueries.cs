namespace TinkloProblemos.API.Database.Queries
{
    public static class ProblemQueries
    {
        public static string Add = @"INSERT INTO problem (Name,Description,Location,Created,StatusId,AssignedUser,InternetUserId) 
                                    VALUES(@Name, @Description, @Location, NOW(), @StatusId, @AssignedUser, @InternetUserId);
                                    SELECT LAST_INSERT_ID();";

    }
}
