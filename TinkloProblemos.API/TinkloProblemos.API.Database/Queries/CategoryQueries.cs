namespace TinkloProblemos.API.Database.Queries
{
    public static class CategoryQueries
    {
        public static string Add = @"INSERT INTO category (Name, Description) 
                                    VALUES(@Name, @Description)";
        public static string GetAll = @"SELECT * FROM category";

        public static string GetById = @"SELECT * FROM category
                                 WHERE Id = @Id";
        public static string Update = @"UPDATE category SET Name = @Name,
                                 Description = @Description
                                   WHERE Id = @Id";
        public static string Delete = @"DELETE FROM category
                                 WHERE Id = @Id";

    }
}
