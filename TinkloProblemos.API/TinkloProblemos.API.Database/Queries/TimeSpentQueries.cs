using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Database.Queries
{
    public static class TimeSpentQueries
    {
        public static string Add = @"INSERT INTO timespent (HoursSpent, Description, DateRecorded, UserId, ProblemId) 
                                    VALUES(@HoursSpent, @Description, @DateRecorded, @UserId, @ProblemId);
                                    SELECT LAST_INSERT_ID();";
        public static string GetAll = @"SELECT * FROM timespent";

        public static string GetPage = @"SELECT * FROM timespent LIMIT @skip, @take";

        public static string GetById = @"SELECT * FROM timespent
                                 WHERE Id = @Id";

        public static string GetProblemTimeSpent = @"SELECT timespent.*,
		users.firstName,
        users.lastName
 FROM timespent inner join users on timespent.userId = users.id
	WHERE problemId = @problemId";

        public static string GetUserTimeSpent = @"SELECT timespent.*,
		users.firstName,
        users.lastName
 FROM timespent inner join users on timespent.userId = users.id
	WHERE userId =@userId";
        public static string Update = @"UPDATE timespent SET Description = @Description,
                                 DateRecorded = @DateRecorded,
                                 HoursSpent = @HoursSpent
                                   WHERE Id = @Id";
        public static string Delete = @"DELETE FROM timespent
                                 WHERE Id = @Id";
    }
}
