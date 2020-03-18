using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Restoran.Hubs;
using Restoran.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Serialization;

namespace Restoran
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Login";
             //   options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });


            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddMvcCore()
                 .AddAuthorization(); // Note - this is on the IMvcBuilder, not the service collection
     
            #region dbContext
            services.AddDbContext<SimpleDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<SimpleDbContext>());

            //services.AddDbContext<SimpleDbContext>(options =>
            //{ options.UseSqlServer(Configuration["Data:ConnectionStrings:server"]); });
            #endregion



            services.AddIdentity<IdentityUser, IdentityRole>()
               //  services.AddDefaultIdentity<IdentityUser>()
               .AddEntityFrameworkStores<SimpleDbContext>()
               .AddDefaultTokenProviders()
               .AddDefaultUI();


            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {

                    options.Conventions.AuthorizeAreaFolder("Identity", "/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Logout");


                });

            services.ConfigureApplicationCookie(options =>
            {
              
                options.LoginPath = $"/Identity/Login";
                options.LogoutPath = $"/Identity/Logout";
                options.AccessDeniedPath = $"/Identity/AccessDenied";
            });

            // using Microsoft.AspNetCore.Identity.UI.Services;
            //  services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
           // app.UseHttpContextItemsMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=about}/{id?}");
                endpoints.MapHub<ChatHub>("/chatHub");

                endpoints.MapControllerRoute(
                name: "RestoranAdmin",
                pattern: "{area:exists}/{controller=Home}/{action=about}/{id?}");
            });
        }
    }
}
