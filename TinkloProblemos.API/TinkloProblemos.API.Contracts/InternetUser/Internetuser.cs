using System.Collections.Generic;

namespace TinkloProblemos.API.Contracts.InternetUser
{
    public class Internetuser
    {
        public Internetuser()
        {
            Internetuserdevice = new HashSet<Internetuserdevice>();
            Problem = new HashSet<Problem.Problem>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public int LastName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string IpAddress { get; set; }

        public ICollection<Internetuserdevice> Internetuserdevice { get; set; }
        public ICollection<Problem.Problem> Problem { get; set; }
    }
}
