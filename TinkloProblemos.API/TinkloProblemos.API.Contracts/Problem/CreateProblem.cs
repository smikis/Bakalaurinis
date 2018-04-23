using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TinkloProblemos.API.Contracts.Problem
{
    public class CreateProblem
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Location { get; set; }
        public string AssignedUser { get; set; }
        public int StatusId { get; set; }
        public int? InternetUserId { get; set; }
        [Required]
        public int Category { get; set; }
        public List<int> Tags { get; set; }
    }
}
