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
using Time.VIewModels;

namespace Time.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employee.Include(e => e.PlaceOfWork).Include(e => e.Position).Include(e => e.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.PlaceOfWork)
                .Include(e => e.Position)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "admin,Employee")]
        public IActionResult Create()
        {
            ViewData["PlaceOfWorkName"] = new SelectList(_context.PlaceOfWork, "Name", "Name");
            ViewData["PositionName"] = new SelectList(_context.Position, "Name", "Name");
            ViewData["UserName"] = new SelectList(_context.Users, "FirstNameAndLastName", "FirstNameAndLastName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Create(EmployeeView employee)
        {
            var NewEmployee = new Employee();
            NewEmployee.User =
                 _context.Users.FirstOrDefault(t => t.FirstNameAndLastName == employee.UserName);
            NewEmployee.PlaceOfWork = _context.PlaceOfWork.FirstOrDefault(t => t.Name == employee.PlaceName);
            NewEmployee.Position = _context.Position.FirstOrDefault(t => t.Name == employee.PositionName);

            _context.Add(NewEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee =  _context.Employee.Include(t=>t.PlaceOfWork)
                .Include(t=>t.User).
                Include(t=>t.Position)
                .First(t=>t.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeView = new EmployeeView();

            ViewData["PlaceOfWorkName"] = new SelectList(_context.PlaceOfWork, "Name", "Name",employee.PlaceOfWork.Name);
            ViewData["PositionName"] = new SelectList(_context.Position, "Name", "Name",employee.Position.Name);
            ViewData["UserName"] = new SelectList(_context.Users, "FirstNameAndLastName", "FirstNameAndLastName",employee.User.FirstNameAndLastName);
            return View(employeeView);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Edit(string id,EmployeeView employee)
        {
            var NewEmployee = new Employee();
            NewEmployee.User =
                 _context.Users.FirstOrDefault(t => t.FirstNameAndLastName == employee.UserName);
            NewEmployee.PlaceOfWork = _context.PlaceOfWork.FirstOrDefault(t => t.Name == employee.PlaceName);
            NewEmployee.Position = _context.Position.FirstOrDefault(t => t.Name == employee.PositionName);
            NewEmployee.Id = id;

            try
            {
                _context.Update(NewEmployee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
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

        // GET: Employees/Delete/5
        [Authorize(Roles = "admin,Employee")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.PlaceOfWork)
                .Include(e => e.Position)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [Authorize(Roles = "admin,Employee")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin,Employee")]
        private bool EmployeeExists(string id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
