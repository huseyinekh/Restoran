using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restoran.Models
{
    public class RestoranSecurityData
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Password { get; set; }
    }
}
