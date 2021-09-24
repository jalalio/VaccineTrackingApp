using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class Warehouse
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(50)"), Required]
        public string Name { get; set; }
        [Column(TypeName = "int"), Required]
        public int Quantity { get; set; }

        // FK
        public Address Address { get; set; }
    }
}
