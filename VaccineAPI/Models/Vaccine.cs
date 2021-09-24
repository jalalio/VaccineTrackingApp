using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class Vaccine
    {
        [Key]
        public Guid VaccineId { get; set; }

        [Column(TypeName="nvarchar(MAX)"), Required]
        public string VaccineName { get; set; }

        [Column(TypeName = "nvarchar(MAX)"), Required]
        public string Manufacturer { get; set; }

        [Column(TypeName = "float"), Required]
        public float StorageTemperature { get; set; }

        [Column(TypeName = "int"), Required]
        public int Doses { get; set; }

        [Column(TypeName = "nvarchar(MAX)"), Required]
        public string Type { get; set; }

        [Column(TypeName = "nvarchar(MAX)"), Required]
        public string AdministrationMethod { get; set; }

        // Foreign Key Default Convention // Navigation property

    }
}
