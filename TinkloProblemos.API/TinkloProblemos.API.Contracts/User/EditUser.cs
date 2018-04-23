using System.ComponentModel.DataAnnotations;

namespace TinkloProblemos.API.Contracts.User
{
    public class EditUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
