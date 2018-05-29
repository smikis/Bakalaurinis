namespace TinkloProblemos.API.Database.Queries
{
    public static class InternetUserQueries
    {
        public static string Add = @"INSERT INTO internetuser (FirstName, LastName, Description, Location, IpAddress,ContractId, StatusId, InternetPlan) 
                                    VALUES(@FirstName, @LastName, @Description, @Location, @IpAddress, @ContractId, @StatusId, @InternetPlan)";
        public static string GetAll = @"SELECT * FROM internetuser";

        public static string GetPage = @"SELECT * FROM internetuser LIMIT @skip, @take";
        public static string GetPageCount = @"SELECT COUNT(*) FROM internetuser";

        public static string Search = @"SELECT * FROM internetuser
WHERE MATCH(FirstName, LastName, Location,IpAddress) AGAINST(@searchQuery IN BOOLEAN MODE);";
        
        public static string SearchPage = @"SELECT * FROM internetuser
WHERE MATCH(FirstName, LastName, Location,IpAddress) AGAINST(@searchQuery IN BOOLEAN MODE)
LIMIT @skip, @take;";
        public static string SearchCount = @"SELECT COUNT(*) FROM internetuser
WHERE MATCH(FirstName, LastName, Location,IpAddress) AGAINST(@searchQuery IN BOOLEAN MODE)
LIMIT @skip, @take;";

        public static string GetById = @"SELECT * FROM internetuser
                                 WHERE Id = @Id";
        public static string Update = @"UPDATE internetuser SET FirstName = @FirstName,
                                 LastName = @LastName,
                                 Description = @Description,
                                 Location = @Location, 
                                 IpAddress = @IpAddress,
                                 ContractId = @ContractId,
                                 StatusId = @StatusId
                                   WHERE Id = @Id";
        public static string Delete = @"DELETE FROM internetuser
                                 WHERE Id = @Id";

    }
}
