using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Location
{
    public class WriteLocation
    {
        public float Lat { get; set; }
        public float Lng { get; set; }
        public string UserId { get; set; }
    }
}
