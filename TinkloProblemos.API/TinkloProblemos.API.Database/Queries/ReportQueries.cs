using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Database.Queries
{
    public class ReportQueries
    {
        public static string GetUserReports = @"SELECT 
COUNT(timespent.userId) as timeSpentCount,
COUNT(DISTINCT timespent.problemId) as problemsSolved,
SUM(timespent.hoursSpent) as time,
MAX(timeSpent.hoursSpent) as maxTime,
MIN(timeSpent.hoursSpent) as minTime,
SUM(timespent.hoursSpent) / COUNT(*) as averageTaskTime,
users.Id as userId,
users.firstName,
users.lastName
FROM timespent inner join users on timespent.userId = users.id
 WHERE (timespent.dateRecorded > @dateFrom && timespent.dateRecorded < @dateTo)
 GROUP BY timespent.userId;";

        public static string GetMostTimeSpentTasks = @"
SELECT problem.id, problem.name,problem.id, SUM(timespent.hoursSpent) as timeSpent
FROM timespent inner join problem on timespent.problemId = problem.id
WHERE timespent.dateRecorded > @dateFrom && timespent.dateRecorded < @dateTo
GROUP BY problem.id
ORDER BY timeSpent desc
LIMIT @limit;";

        public static string GetInternetClientWithMostTimeSpent = @"
SELECT problem.internetUserId, internetUser.firstName as firstName, internetUser.lastName as lastName, SUM(timespent.hoursSpent) as timeSpent
FROM timespent inner join problem on timespent.problemId = problem.id
inner join internetUser on internetUser.id = problem.internetUserId
WHERE timespent.dateRecorded > @dateFrom && timespent.dateRecorded < @dateTo
GROUP BY problem.id
ORDER BY timeSpent desc
LIMIT @limit;";


    }
}
