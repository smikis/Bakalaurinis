namespace TinkloProblemos.API.Database.Queries
{
    public static class InternetUserQueries
    {
        public static string Add = @"INSERT INTO internetuser (FirstName, LastName, Description, Location, IpAddress) 
                                    VALUES(@FirstName, @LastName, @Description, @Location, @IpAddress)";
        public static string GetAll = @"SELECT * FROM InternetUser";

        public static string GetPage = @"SELECT * FROM InternetUser LIMIT @skip, @take";

        public static string GetById = @"SELECT * FROM internetuser
                                 WHERE Id = @Id";
        public static string Update = @"UPDATE internetuser SET FirstName = @FirstName,
                                 LastName = @LastName,
                                 Description = @Description,
                                 Location = @Location, 
                                 IpAddress = @IpAddress
                                   WHERE Id = @Id";
        public static string Delete = @"DELETE FROM internetuser
                                 WHERE Id = @Id";

    }
}
