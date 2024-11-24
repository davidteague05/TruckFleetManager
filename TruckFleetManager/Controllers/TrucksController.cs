using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TruckFleetManager.Data;
using TruckFleetManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace TruckFleetManager.Controllers
{
    [Authorize]
    public class TrucksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrucksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trucks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Truck.Include(t => t.TruckType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Trucks/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Truck
                .Include(t => t.TruckType)
                .FirstOrDefaultAsync(m => m.TruckId == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // GET: Trucks/Create
        public IActionResult Create()
        {
            ViewData["TruckTypeId"] = new SelectList(_context.Type.OrderBy(t => t.Name), "TruckTypeId", "Name");
            return View();
        }

        // POST: Trucks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TruckId,Year,Brand,Model,Transmission,LastService,TruckTypeId")] Truck truck, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var fileName = UploadImage(Image);
                    truck.Image = fileName;
                }
                _context.Add(truck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TruckTypeId"] = new SelectList(_context.Type, "TruckTypeId", "TruckTypeId", truck.TruckTypeId);
            return View(truck);
        }

        // GET: Trucks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Truck.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }
            ViewData["TruckTypeId"] = new SelectList(_context.Type.OrderBy(t => t.Name), "TruckTypeId", "Name", truck.TruckTypeId);
            return View(truck);
        }

        // POST: Trucks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TruckId,Year,Brand,Model,Transmission,LastService,TruckTypeId")] Truck truck, IFormFile? Image, String? CurrentImage)
        {
            if (id != truck.TruckId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null)
                    {
                        var fileName = UploadImage(Image);
                        truck.Image = fileName;
                    }
                    else
                    {
                        // keep current photo if any
                        truck.Image = CurrentImage;
                    }

                    _context.Update(truck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckExists(truck.TruckId))
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
            ViewData["TruckTypeId"] = new SelectList(_context.Type, "TruckTypeId", "TruckTypeId", truck.TruckTypeId);
            return View(truck);
        }

        // GET: Trucks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Truck
                .Include(t => t.TruckType)
                .FirstOrDefaultAsync(m => m.TruckId == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // POST: Trucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var truck = await _context.Truck.FindAsync(id);
            if (truck != null)
            {
                _context.Truck.Remove(truck);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckExists(int id)
        {
            return _context.Truck.Any(e => e.TruckId == id);
        }

        private string UploadImage(IFormFile Image)
        {  
            var filePath = Path.GetTempFileName();
            var fileName = Guid.NewGuid().ToString() + "-" + Image.FileName;
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\trucks\\" + fileName;

            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }
            return fileName;
        }
    }
}
