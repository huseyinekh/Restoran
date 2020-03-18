using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restoran.Areas.RestoranAdmin.Models
{
    public class User:IdentityUser
    {
        public int RestoranUserId { get; set; }
    }
}
