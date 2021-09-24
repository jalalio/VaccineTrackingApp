using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccineAPI.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Email { get; set; }
    }
}
