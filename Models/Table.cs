using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restoran.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        [Required]
        public string TableName { get; set; }

        [StringLength(10)]
        [Required]
        public string TableNumber { get; set; }
        [StringLength(400)]
        public string Qrcode { get; set; }
        [StringLength(250)]
        public string ImageLink { get; set; }
    }
}
