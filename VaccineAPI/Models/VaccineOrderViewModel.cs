using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class VaccineOrderViewModel
    {
        public string UserName { get; set; }
        public string VaccineName { get; set; }

        public Order Order { get; set; }
    }
}
