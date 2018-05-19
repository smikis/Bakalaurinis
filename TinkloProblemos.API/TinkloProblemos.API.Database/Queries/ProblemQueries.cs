namespace TinkloProblemos.API.Database.Queries
{
    public static class ProblemQueries
    {
        public static string Add = @"INSERT INTO problem (Name,Description,Location,Created,StatusId,AssignedUser,InternetUserId) 
                                    VALUES(@Name, @Description, @Location, NOW(), @StatusId, @AssignedUser, @InternetUserId);
                                    SELECT LAST_INSERT_ID();";

        public static string UpdateAssignedUser = @"UPDATE bakalaurinis.problem
SET assignedUser = @userId
WHERE id = @problemId;";

        public static string UpdateDescription = @"UPDATE bakalaurinis.problem
SET description = @description
WHERE id = @problemId;";

        public static string UpdateInternetUser = @"UPDATE bakalaurinis.problem
SET internetUserId = @internetUserId
WHERE id = @problemId;";
        public static string UpdateStatus = @"UPDATE bakalaurinis.problem
SET statusId = @statusId
WHERE id = @problemId;";

        public static string GetProblem =
            @"SELECT problem.Id, problem.StatusId, problem.Name,problem.Location, problem.Created, problem.Description, problem.Created, problem.internetuserid, category.Id as categoryId, category.Name as categoryName,
users.Id as assignedUserId, users.FirstName as assignedUserFirstName, users.Email as assignedUserEmail, status.name as status
FROM problem left join category_problem on problem.id = category_problem.problemId
left join category on category.id = category_problem.categoryId
left join users on users.id = problem.assignedUser
inner join status on status.id = problem.statusId
where problem.id = @id;";

        public static string GetAllUnfiltered =
            @"SELECT problem.Id, problem.Name,problem.Location, problem.Description, problem.Created, category.Id as categoryId, category.Name as categoryName,
users.Id as assignedUserId, users.FirstName as assignedUserFirstName, users.Email as assignedUserEmail, status.name as status
FROM problem left join category_problem on problem.id = category_problem.problemId
left join category on category.id = category_problem.categoryId
left join users on users.id = problem.assignedUser
inner join status on status.id = problem.statusId
LIMIT @skip, @take;";

        public static string GetFilteredPage =
            @"SELECT problem.Id, problem.Name,problem.Location,  problem.Description, problem.Created, category.Id as categoryId, category.Name as categoryName,
users.Id as assignedUserId, users.FirstName as assignedUserFirstName, users.Email as assignedUserEmail, status.name as status
FROM problem left join category_problem on problem.id = category_problem.problemId
left join category on category.id = category_problem.categoryId
left join users on users.id = problem.assignedUser
inner join status on status.id = problem.statusId
where ((@categoryName is not null AND category.Name = @categoryName) OR @categoryName is null)
AND ((@status is not null AND status.Name = @status) OR @status is null)
AND ((@assignedUser is not null AND users.Id = @assignedUser) OR @assignedUser is null)
AND ((@dateFrom is not null AND problem.Created >= @dateFrom) OR @dateFrom is null)
AND ((@dateTo is not null AND problem.Created <= @dateTo) OR @dateTo is null)
LIMIT @skip, @take";
        public static string GetFilteredPageCount =
            @"SELECT COUNT(*)
FROM problem left join category_problem on problem.id = category_problem.problemId
left join category on category.id = category_problem.categoryId
left join users on users.id = problem.assignedUser
inner join status on status.id = problem.statusId
where ((@categoryName is not null AND category.Name = @categoryName) OR @categoryName is null)
AND ((@status is not null AND status.Name = @status) OR @status is null)
AND ((@assignedUser is not null AND users.Id = @assignedUser) OR @assignedUser is null)
AND ((@dateFrom is not null AND problem.Created >= @dateFrom) OR @dateFrom is null)
AND ((@dateTo is not null AND problem.Created <= @dateTo) OR @dateTo is null)";

        public static string GetFiltered =
            @"SELECT problem.Id, problem.Name,problem.Location,  problem.Description, problem.Created, category.Id as categoryId, category.Name as categoryName,
users.Id as assignedUserId, users.FirstName as assignedUserFirstName, users.Email as assignedUserEmail, status.name as status
FROM problem left join category_problem on problem.id = category_problem.problemId
left join category on category.id = category_problem.categoryId
left join users on users.id = problem.assignedUser
inner join status on status.id = problem.statusId
where ((@categoryName is not null AND category.Name = @categoryName) OR @categoryName is null)
AND ((@status is not null AND status.Name = @status) OR @status is null)
AND ((@assignedUser is not null AND users.Id = @assignedUser) OR @assignedUser is null)
AND ((@dateFrom is not null AND problem.Created >= @dateFrom) OR @dateFrom is null)
AND ((@dateTo is not null AND problem.Created <= @dateTo) OR @dateTo is null)";

        public static string GetFilteredSearch =
            @"SELECT problem.Id, problem.Name,problem.Location,  problem.Description, problem.Created, category.Id as categoryId, category.Name as categoryName,
users.Id as assignedUserId, users.FirstName as assignedUserFirstName, users.Email as assignedUserEmail, status.name as status
FROM problem left join category_problem on problem.id = category_problem.problemId
left join category on category.id = category_problem.categoryId
left join users on users.id = problem.assignedUser
inner join status on status.id = problem.statusId
where ((@categoryName is not null AND category.Name = @categoryName) OR @categoryName is null)
AND ((@status is not null AND status.Name = @status) OR @status is null)
AND ((@assignedUser is not null AND users.Id = @assignedUser) OR @assignedUser is null)
AND ((@dateFrom is not null AND problem.Created >= @dateFrom) OR @dateFrom is null)
AND ((@dateTo is not null AND problem.Created <= @dateTo) OR @dateTo is null)
AND MATCH(problem.Name,problem.Description,problem.Location) AGAINST(@searchQuery IN BOOLEAN MODE)
LIMIT @skip, @take";

        public static string GetFilteredSearchCount =
            @"SELECT COUNT(*)
FROM problem left join category_problem on problem.id = category_problem.problemId
left join category on category.id = category_problem.categoryId
left join users on users.id = problem.assignedUser
inner join status on status.id = problem.statusId
where ((@categoryName is not null AND category.Name = @categoryName) OR @categoryName is null)
AND ((@status is not null AND status.Name = @status) OR @status is null)
AND ((@assignedUser is not null AND users.Id = @assignedUser) OR @assignedUser is null)
AND ((@dateFrom is not null AND problem.Created >= @dateFrom) OR @dateFrom is null)
AND ((@dateTo is not null AND problem.Created <= @dateTo) OR @dateTo is null)
AND MATCH(problem.Name,problem.Description,problem.Location) AGAINST(@searchQuery IN BOOLEAN MODE)";

        public static string GetFilteredUser =
            @"SELECT problem.Id, problem.Name,problem.Location,  problem.Description, problem.Created, category.Id as categoryId, category.Name as categoryName,
users.Id as assignedUserId, users.FirstName as assignedUserFirstName, users.Email as assignedUserEmail, status.name as status
FROM problem left join category_problem on problem.id = category_problem.problemId
left join category on category.id = category_problem.categoryId
left join users on users.id = problem.assignedUser
inner join status on status.id = problem.statusId
where ((@categoryName is not null AND category.Name = @categoryName) OR @categoryName is null)
AND ((@status is not null AND status.Name = @status) OR @status is null)
AND users.Id = @assignedUser";

    }
}
