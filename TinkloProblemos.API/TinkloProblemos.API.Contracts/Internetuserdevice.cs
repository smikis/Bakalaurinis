using System;

namespace TinkloProblemos.API.Contracts
{
    public class Internetuserdevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string MacAddress { get; set; }
        public string Description { get; set; }
        public int InternetUserId { get; set; }
        public DateTime WarrantyExpiration { get; set; }

        public Internetuser InternetUser { get; set; }
    }
}
