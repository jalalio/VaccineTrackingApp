using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(250)"), Required]
        public string Address1 { get; set; }
        #nullable enable
        [Column(TypeName = "nvarchar(250)"), Required]
        public string? Address2 { get; set; }
        #nullable disable
        [Column(TypeName = "nvarchar(50)"), Required]
        public string Suburb { get; set; }
        public int Postcode { get; set; }

        // FK
        public User User { get; set; }
    }
}
