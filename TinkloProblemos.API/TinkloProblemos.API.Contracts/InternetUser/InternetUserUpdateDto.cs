using System.ComponentModel.DataAnnotations;

namespace TinkloProblemos.API.Contracts.InternetUser
{
    public class InternetUserUpdateDto
    {
        public string FirstName { get; set; }
        [Required]
        public int LastName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string IpAddress { get; set; }
    }
}
