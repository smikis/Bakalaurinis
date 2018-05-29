using System;
using System.ComponentModel.DataAnnotations;

namespace TinkloProblemos.API.Contracts.InternetUser
{
    public class InternetUserDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }

        public string IpAddress { get; set; }
        public DateTime Created { get; set; }
        public string InternetPlan { get; set; }
        public int? ContractId { get; set; }
        public int StatusId { get; set; } = 1;
    }
}
