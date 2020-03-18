using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restoran.DAL;
using Restoran.Models;

namespace Restoran.Controllers
{


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SimpleDbContext _context;
        public HomeController(ILogger<HomeController> logger,SimpleDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public  IActionResult Index()
        {
            var model = new TablePassword();
          
            return  View(model);

        }


        [HttpPost]
        public IActionResult Index(string Id,string password)
        {
            var model = new TablePassword();
            model.TableNum = Id;
            model.Password = password;
            return RedirectToAction("ResturantSerVice",model);

        }

        public async Task<IActionResult>ResturantSerVice(TablePassword tablePassword)
        {
           
            var _tableNumbers = _context.RestoranSecurityDatas;
            foreach (var item in _tableNumbers)
            {
                if (tablePassword .Password== item.Password)
                {
                   
                    var CurrentTable = await _context.Tables.FirstOrDefaultAsync(i => i.TableNumber==tablePassword.TableNum);
                    if (CurrentTable!=null)
                    {
                        return View(CurrentTable);
                    }
                    
                }
            }
            return RedirectToAction("About");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            var model = _context.AboutResturant;
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
