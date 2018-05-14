using System;

namespace TinkloProblemos.API.Contracts.User
{
    public class GetUserExtended
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? RegistrationDate { get; set; }

    }
}
