using System;

namespace TinkloProblemos.API.Identity.Entities
{
    public class ApplicationRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}