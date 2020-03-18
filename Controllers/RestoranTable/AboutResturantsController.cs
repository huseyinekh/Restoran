using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restoran.DAL;
using Restoran.Models;

namespace Restoran.Controllers.RestoranTable
{
   
    public class AboutResturantsController : Controller
    {
        private readonly SimpleDbContext _context;

        public AboutResturantsController(SimpleDbContext context)
        {
            _context = context;
        }

        // GET: AboutResturants
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }
            return View(await _context.AboutResturant.ToListAsync());
        }

        // GET: AboutResturants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }



            if (id == null)
            {
                return NotFound();
            }

            var aboutResturant = await _context.AboutResturant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutResturant == null)
            {
                return NotFound();
            }

            return View(aboutResturant);
        }

        // GET: AboutResturants/Create
        public IActionResult Create()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }


            return View();
        }

        // POST: AboutResturants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Title,About,Description")] AboutResturant aboutResturant)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }



            if (ModelState.IsValid)
            {
                _context.Add(aboutResturant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutResturant);
        }

        // GET: AboutResturants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }


            if (id == null)
            {
                return NotFound();
            }

            var aboutResturant = await _context.AboutResturant.FindAsync(id);
            if (aboutResturant == null)
            {
                return NotFound();
            }
            return View(aboutResturant);
        }

        // POST: AboutResturants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title,About,Description")] AboutResturant aboutResturant)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }


            if (id != aboutResturant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aboutResturant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutResturantExists(aboutResturant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aboutResturant);
        }

        // GET: AboutResturants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }


            if (id == null)
            {
                return NotFound();
            }

            var aboutResturant = await _context.AboutResturant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutResturant == null)
            {
                return NotFound();
            }

            return View(aboutResturant);
        }

        // POST: AboutResturants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }


            var aboutResturant = await _context.AboutResturant.FindAsync(id);
            _context.AboutResturant.Remove(aboutResturant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutResturantExists(int id)
        {

            return _context.AboutResturant.Any(e => e.Id == id);
        }
    }
}
