using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restoran.Areas.RestoranAdmin.Models;
using Restoran.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restoran.DAL
{
    public class SimpleDbContext: IdentityDbContext<IdentityUser>
    {
        public SimpleDbContext(DbContextOptions<SimpleDbContext>options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          
        }
        public DbSet<Table> Tables { get; set; }
        public DbSet<RestoranSecurityData> RestoranSecurityDatas { get; set; }
        public DbSet<AboutResturant> AboutResturant { get; set; }
        public DbSet<SiteAbout> SiteAbouts { get; set; }




    }
}
