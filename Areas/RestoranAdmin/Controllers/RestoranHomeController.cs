using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restoran.DAL;
namespace Restoran.Areas.RestoranAdmin.Controllers
{

  

    [Area("RestoranAdmin")]
   
    public class RestoranHomeController : Controller
    {
        private readonly SimpleDbContext _context;
        public RestoranHomeController(SimpleDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }

            var Tables = _context.Tables;
            return View(Tables);
        }
    }
}