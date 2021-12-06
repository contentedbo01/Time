using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Time.Data;
using Time.Models;
using Time.VIewModels;

namespace Time.Controllers
{
    public class TimeWorkedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeWorkedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimeWorkeds
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TimeWorked.Include(t => t.Employee).Include(t => t.Employee.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TimeWorkeds/Details/5
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeWorked = await _context.TimeWorked
                .Include(t => t.Employee).Include(t => t.Employee.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeWorked == null)
            {
                return NotFound();
            }

            return View(timeWorked);
        }

        // GET: TimeWorkeds/Create
        [Authorize(Roles = "admin,Employee")]
        public IActionResult Create()
        {
            ViewData["UserName"] = new SelectList(_context.Users, "FirstNameAndLastName", "FirstNameAndLastName");
            return View();
        }

        // POST: TimeWorkeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Create(TimeWorkedView timeWorked)
        {
            var NewTimeWorked = new TimeWorked();
            NewTimeWorked.Start = timeWorked.Start;
            NewTimeWorked.End = timeWorked.End;

            NewTimeWorked.EmployeeId = _context.Employee.Include(t => t.User).FirstOrDefault(t => t.User.FirstNameAndLastName == timeWorked.UserName).Id;


            _context.Add(NewTimeWorked);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: TimeWorkeds/Edit/5
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeWorked = _context.TimeWorked.Include(t => t.Employee.User).First(t => t.Id == id);
            if (timeWorked == null)
            {
                return NotFound();
            }
            var NewTimeWorked = new TimeWorkedView();
            ViewData["UserName"] = new SelectList(_context.Users, "FirstNameAndLastName", "FirstNameAndLastName", timeWorked.Employee.User.FirstNameAndLastName);
            return View(NewTimeWorked);
        }

        // POST: TimeWorkeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Edit(string id, TimeWorkedView timeWorked)
        {

            var NewTimeWorked = new TimeWorked();

            NewTimeWorked.End = timeWorked.End;
            NewTimeWorked.Start = timeWorked.Start;
            var EmployeeId = _context.Employee.First(t => t.User.FirstNameAndLastName == timeWorked.UserName).Id;
            NewTimeWorked.EmployeeId = EmployeeId;

            if (id != EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeWorked);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: TimeWorkeds/Delete/5
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeWorked = await _context.TimeWorked
                .Include(t => t.Employee).Include(t => t.Employee.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeWorked == null)
            {
                return NotFound();
            }

            return View(timeWorked);
        }

        // POST: TimeWorkeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var timeWorked = await _context.TimeWorked.FindAsync(id);
            _context.TimeWorked.Remove(timeWorked);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin,Employee")]
        private bool TimeWorkedExists(string id)
        {
            return _context.TimeWorked.Any(e => e.Id == id);
        }
    }
}
