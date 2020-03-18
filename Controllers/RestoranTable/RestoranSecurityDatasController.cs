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


    public class RestoranSecurityDatasController : Controller
    {
        private readonly SimpleDbContext _context;

        public RestoranSecurityDatasController(SimpleDbContext context)
        {
            _context = context;
        }

        // GET: RestoranSecurityDatas
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }



            return View(await _context.RestoranSecurityDatas.ToListAsync());
        }

        // GET: RestoranSecurityDatas/Details/5
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

            var restoranSecurityData = await _context.RestoranSecurityDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restoranSecurityData == null)
            {
                return NotFound();
            }

            return View(restoranSecurityData);
        }

        // GET: RestoranSecurityDatas/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }



            return View();
        }

        // POST: RestoranSecurityDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Password")] RestoranSecurityData restoranSecurityData)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }



            if (ModelState.IsValid)
            {
                _context.Add(restoranSecurityData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restoranSecurityData);
        }

        // GET: RestoranSecurityDatas/Edit/5
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

            var restoranSecurityData = await _context.RestoranSecurityDatas.FindAsync(id);
            if (restoranSecurityData == null)
            {
                return NotFound();
            }
            return View(restoranSecurityData);
        }

        // POST: RestoranSecurityDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Password")] RestoranSecurityData restoranSecurityData)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }



            if (id != restoranSecurityData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restoranSecurityData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestoranSecurityDataExists(restoranSecurityData.Id))
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
            return View(restoranSecurityData);
        }

        // GET: RestoranSecurityDatas/Delete/5
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

            var restoranSecurityData = await _context.RestoranSecurityDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restoranSecurityData == null)
            {
                return NotFound();
            }

            return View(restoranSecurityData);
        }

        // POST: RestoranSecurityDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }



            var restoranSecurityData = await _context.RestoranSecurityDatas.FindAsync(id);
            _context.RestoranSecurityDatas.Remove(restoranSecurityData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestoranSecurityDataExists(int id)
        {
   



            return _context.RestoranSecurityDatas.Any(e => e.Id == id);
        }
    }
}
