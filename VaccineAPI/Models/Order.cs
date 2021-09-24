using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [Column(TypeName = "nvarchar(450)"), Required]
        //public string UserId { get; set; }
        //[Column(TypeName = "uniqueidentifier"), Required]
        //public Guid VaccineId { get; set; }
        //[Column(TypeName = "int"), Required]
        public int Quantity { get; set; }
        [Column(TypeName = "datetime"), Required]
        public DateTime Date { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; }

        // Foreign Key Default Convention // Navigation property
        [Required]
        public User User { get; set; }
        public ICollection<VaccineOrder> VaccineOrders;
    }
}
