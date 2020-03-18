using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restoran.DAL;
using Restoran.Models;
using static Restoran.Tools.QrCodeConverter;
using Restoran.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using static Restoran.Tools.RemoveImage;
using Microsoft.AspNetCore.Authorization;
using QRCoder;
using System.Drawing.Imaging;

namespace Restoran.Controllers.RestoranTable
{
   
    public class TablesController : Controller
    {
        private readonly IWebHostEnvironment _environment;


        private readonly SimpleDbContext _context;


        public TablesController(SimpleDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Tables
      
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated==false)
            {
                return RedirectToAction("Login", "Identity");

            }


            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData  qrCodeData = qrGenerator.CreateQrCode("TEXT", QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    var image = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    ViewBag.Code = image;
           }
            }
            return View(await _context.Tables.ToListAsync());

            




        }

        // GET: Tables/Details/5
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

            var table = await _context.Tables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // GET: Tables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TableName,TableNumber,Qrcode")] Table table)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }


            var Site = _context.SiteAbouts.First().SiteUrl;
            var QrcodeText = Site+"/Home/Index?Id=" + table.TableNumber;
            var Qrcode = QrCodeToImage(QrcodeText);


            table.ImageLink = await Qrcode.Save(_environment.WebRootPath, "assets", "image");

            

            if (ModelState.IsValid)
            {
                _context.Tables.Add(table);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(table);
        }

        // GET: Tables/Edit/5
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

            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TableName,TableNumber,Qrcode,ImageLink")] Table table)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }


            if (id != table.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Tables.Update(table);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(table.Id))
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
            return View(table);
        }

        // GET: Tables/Delete/5
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
           
            var table = await _context.Tables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Identity");
            }


            var table = await _context.Tables.FindAsync(id);
            RemoveImg(_environment.WebRootPath, table.ImageLink);
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableExists(int id)
        {
            return _context.Tables.Any(e => e.Id == id);
        }
    }
}
