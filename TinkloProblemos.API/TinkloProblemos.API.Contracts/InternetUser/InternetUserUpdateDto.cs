using System.ComponentModel.DataAnnotations;

namespace TinkloProblemos.API.Contracts.InternetUser
{
    public class InternetUserUpdateDto
    {
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        public string IpAddress { get; set; }
        public int? ContractId { get; set; }
        public int StatusId { get; set; } = 1;
    }
}
