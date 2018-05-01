using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Problem
{
    public class GetProblem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string AssignedUserId { get; set; }
        public string AssignedUserFirstName { get; set; }
        public string AssignedUserEmail { get; set; }
        public int? InternetUserId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}
