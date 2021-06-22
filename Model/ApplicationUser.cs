using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace codetask.Model
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        public string FullName { get; set; }
        public string City { get; set; }
    }
}
