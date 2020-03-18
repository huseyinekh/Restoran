
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Restoran.Areas.RestoranAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restoran.DAL
{
    public class DbInitializer
    {
        public async static Task SeedAsync(SimpleDbContext context,
                                             UserManager<User> role,
                                             RoleManager<IdentityRole> roleManager,
                                             IConfiguration configuration)
        {
            await context.Database.EnsureCreatedAsync();

        }
    }
}