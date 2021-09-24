using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class User : IdentityUser
    {
        // Can add custom attributes here

        // Foreign Key Default Convention // Navigation property
        public ICollection<Order> Orders { get; set; }
    }
}
