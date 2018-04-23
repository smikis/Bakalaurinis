using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TinkloProblemos.API.Contracts.Tag
{
    public class CreateTagDto
    {
        [Required]
        public string Name { get; set; }
    }
}
