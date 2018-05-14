using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Location
{
    public class GetLocation
    {
        public float Lat { get; set; }
        public float Lng { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
