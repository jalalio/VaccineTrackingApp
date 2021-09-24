using System;

namespace VaccineAPI.Models
{
    public class VaccineOrder
    {
        public Guid VaccineId { get; set; }
        public Vaccine Vaccine { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
