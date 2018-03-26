using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Database
{
    public partial class Internetuser
    {
        public Internetuser()
        {
            Internetuserdevice = new HashSet<Internetuserdevice>();
            Problem = new HashSet<Problem>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public int LastName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string IpAddress { get; set; }

        public ICollection<Internetuserdevice> Internetuserdevice { get; set; }
        public ICollection<Problem> Problem { get; set; }
    }
}
