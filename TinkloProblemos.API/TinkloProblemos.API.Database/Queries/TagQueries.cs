using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Database.Queries
{
    public static class TagQueries
    {
        public static string Add = @"INSERT INTO tag(Name)
                                        VALUES(@Name);";
        public static string GetAll = @"SELECT * FROM tag";

        public static string GetProblemTags = @"SELECT tag.id,tag.name FROM 
tag inner join problem_tag on tag.id = problem_tag.tagId
WHERE problem_tag.problemId = @ProblemId;";

        public static string Delete =
@"DELETE FROM problem_tag
WHERE TagId =  @Id;
DELETE FROM tag
WHERE Id = @Id;";

        public static string AddToProblem =
@"INSERT INTO problem_tag(TagId,ProblemId)
VALUES(@TagId,@ProblemId);";
        public static string RemoveFromProblem =
            @"DELETE FROM problem_tag
WHERE TagId = (@TagId AND ProblemId=@ProblemId;";
    }
}
