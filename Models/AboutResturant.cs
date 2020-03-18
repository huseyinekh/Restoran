using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restoran.Models
{
    public class AboutResturant
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string  Image { get; set; }
        [StringLength(200)]

        public string Title { get; set; }
        [StringLength(1000)]

        public string About { get; set; }
        [StringLength(3000)]

        public string Description { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }


    }
}
