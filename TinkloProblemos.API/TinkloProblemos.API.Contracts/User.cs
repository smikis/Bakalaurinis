using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Database
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Problem = new HashSet<Problem>();
            Timespent = new HashSet<Timespent>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTimeOffset Created { get; set; }
        public sbyte IsDisabled { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<Problem> Problem { get; set; }
        public ICollection<Timespent> Timespent { get; set; }
    }
}
