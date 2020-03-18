using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restoran.DAL;
using Restoran.Models;

namespace Restoran.Controllers
{
 
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly SignInManager<IdentityUser> signInManager;

        private readonly UserManager<IdentityUser> userManager;
        private readonly SimpleDbContext _context;

        public IdentityController(UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager,
           SimpleDbContext context,
           ILogger<IdentityController> logger)

        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterModel model)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        // Copy data from RegisterViewModel to IdentityUser
        //        var user = new IdentityUser
        //        {
        //            UserName = model.UserName,
        //            Email = model.UserName
        //        };

        //        // Store user data in AspNetUsers database table
        //        var result = await userManager.CreateAsync(user, model.Password);

        //        // If user is successfully created, sign-in the user using
        //        // SignInManager and redirect to index action of HomeController
        //        if (result.Succeeded)
        //        {
                   
                 
        //            await signInManager.SignInAsync(user, isPersistent: false);
        //            return RedirectToAction("index", "home");
        //        }

        //        // If there are any errors, add them to the ModelState object
        //        // which will be displayed by the validation summary tag helper
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    return View(model);
        //}



        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.UserName);
            if (user != null &&
                await userManager.CheckPasswordAsync(user, model.Password))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(identity));

                var siteLink = _context.SiteAbouts.FirstOrDefault().SiteUrl;
                      string link = siteLink + "/RestoranAdmin/RestoranHome/index";

                return RedirectToAction("index", "tables");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }



            //if (ModelState.IsValid)
            //{
            //    var result = await signInManager.PasswordSignInAsync(
            //        model.UserName, model.Password, model.RememberMe, false);

            //    //var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,
            //    //    lockoutOnFailure: true);

            //    if (result.Succeeded)
            //    {
            //        var siteLink = _context.SiteAbouts.FirstOrDefault().SiteUrl;
            //        string link = siteLink + "/RestoranAdmin/RestoranHome/index";
                   
            //        Response.Redirect("http://"+link);
            //    }

            //    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            //}

          //  return View(model);
        }
    }
}