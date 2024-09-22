using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCapp.Db;
using MVCapp.Models;

namespace MVCapp.Controllers
{
    public class VoorbeeldsController : Controller
    {
        private readonly VoorbeeldDb _context;

        public VoorbeeldsController(VoorbeeldDb context)
        {
            _context = context;
        }

        // GET: Voorbeelds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Voorbeelden.ToListAsync());
        }

        // GET: Voorbeelds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voorbeeld = await _context.Voorbeelden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voorbeeld == null)
            {
                return NotFound();
            }

            return View(voorbeeld);
        }

        // GET: Voorbeelds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Voorbeelds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedOn")] Voorbeeld voorbeeld)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voorbeeld);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voorbeeld);
        }

        // GET: Voorbeelds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voorbeeld = await _context.Voorbeelden.FindAsync(id);
            if (voorbeeld == null)
            {
                return NotFound();
            }
            return View(voorbeeld);
        }

        // POST: Voorbeelds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedOn")] Voorbeeld voorbeeld)
        {
            if (id != voorbeeld.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voorbeeld);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoorbeeldExists(voorbeeld.Id))
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
            return View(voorbeeld);
        }

        // GET: Voorbeelds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voorbeeld = await _context.Voorbeelden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voorbeeld == null)
            {
                return NotFound();
            }

            return View(voorbeeld);
        }

        // POST: Voorbeelds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voorbeeld = await _context.Voorbeelden.FindAsync(id);
            if (voorbeeld != null)
            {
                _context.Voorbeelden.Remove(voorbeeld);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoorbeeldExists(int id)
        {
            return _context.Voorbeelden.Any(e => e.Id == id);
        }
    }
}
