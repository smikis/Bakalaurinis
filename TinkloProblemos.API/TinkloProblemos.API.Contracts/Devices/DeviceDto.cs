using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Devices
{
    public class DeviceDto
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string MacAddress { get; set; }
        public string Description { get; set; }
        public int? InternetUserId { get; set; }
        public DateTime WarrantyExpiration { get; set; }
    }
}
