﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class OrderViewModel
    {
        [Required]
        public Guid VaccineId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
