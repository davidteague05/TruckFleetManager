using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TruckFleetManager.Data;
using TruckFleetManager.Models;

namespace TruckFleetManager.Controllers
{
    [Authorize]
    public class TruckTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TruckTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TruckTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Type.ToListAsync());
        }

        // GET: TruckTypes/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckType = await _context.Type
                .FirstOrDefaultAsync(m => m.TruckTypeId == id);
            if (truckType == null)
            {
                return NotFound();
            }

            return View(truckType);
        }

        // GET: TruckTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TruckTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TruckTypeId,Name")] TruckType truckType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truckType);
        }

        // GET: TruckTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckType = await _context.Type.FindAsync(id);
            if (truckType == null)
            {
                return NotFound();
            }
            return View(truckType);
        }

        // POST: TruckTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TruckTypeId,Name")] TruckType truckType)
        {
            if (id != truckType.TruckTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckTypeExists(truckType.TruckTypeId))
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
            return View(truckType);
        }

        // GET: TruckTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckType = await _context.Type
                .FirstOrDefaultAsync(m => m.TruckTypeId == id);
            if (truckType == null)
            {
                return NotFound();
            }

            return View(truckType);
        }

        // POST: TruckTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var truckType = await _context.Type.FindAsync(id);
            if (truckType != null)
            {
                _context.Type.Remove(truckType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckTypeExists(int id)
        {
            return _context.Type.Any(e => e.TruckTypeId == id);
        }
    }
}
