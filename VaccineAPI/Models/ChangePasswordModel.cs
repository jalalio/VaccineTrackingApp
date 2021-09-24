﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
