using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Time.Data;
using Time.Models;

namespace Time.Controllers
{
    public class PlaceOfWorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaceOfWorksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlaceOfWorks
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlaceOfWork.Include(p => p.Owner).Include(_ => _.Owner.Employee.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlaceOfWorks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeOfWork = await _context.PlaceOfWork
                .Include(p => p.Owner).Include(_ => _.Owner.Employee.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeOfWork == null)
            {
                return NotFound();
            }

            return View(placeOfWork);
        }

        // GET: PlaceOfWorks/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Owner, "Id", "Id");
            return View();
        }

        // POST: PlaceOfWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,OwnerId")] PlaceOfWork placeOfWork)
        {
            _context.Add(placeOfWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: PlaceOfWorks/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeOfWork = await _context.PlaceOfWork.FindAsync(id);
            if (placeOfWork == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Owner, "Id", "Id", placeOfWork.OwnerId);
            return View(placeOfWork);
        }

        // POST: PlaceOfWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,OwnerId")] PlaceOfWork placeOfWork)
        {
            if (id != placeOfWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placeOfWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceOfWorkExists(placeOfWork.Id))
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
            ViewData["OwnerId"] = new SelectList(_context.Owner, "Id", "Id", placeOfWork.OwnerId);
            return View(placeOfWork);
        }

        // GET: PlaceOfWorks/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeOfWork = await _context.PlaceOfWork
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeOfWork == null)
            {
                return NotFound();
            }

            return View(placeOfWork);
        }

        // POST: PlaceOfWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var placeOfWork = await _context.PlaceOfWork.FindAsync(id);
            _context.PlaceOfWork.Remove(placeOfWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceOfWorkExists(string id)
        {
            return _context.PlaceOfWork.Any(e => e.Id == id);
        }
    }
}
