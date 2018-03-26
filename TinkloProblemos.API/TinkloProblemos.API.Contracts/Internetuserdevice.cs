using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Database
{
    public partial class Internetuserdevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string MacAddress { get; set; }
        public string Description { get; set; }
        public int InternetUserId { get; set; }

        public Internetuser InternetUser { get; set; }
    }
}
