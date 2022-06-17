using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchedulePlan.DataAccess;
using SchedulePlan.Models;

namespace ScheduleCreator.Controllers
{

    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly AppDBContext _context;

        public SubjectsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var currUser = _context.Users.First(x => x.Email.Equals(User.Identity.Name));
            var subjects = await _context.Subjects.Where(plan => plan.User.Equals(currUser)).ToListAsync();
            return View(subjects);
        }

        // GET: Plans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subjects = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjects == null)
            {
                return NotFound();
            }

            return View(subjects);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            var newSubject = new Subject();
            var userEmail = User?.Identity?.Name;
            newSubject.User = _context.Users.First(x => x.Email.Equals(userEmail));
            return View(newSubject);
        }

        // POST: Plans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Subject subject)
        {
            //var planId = _context.Plans.Max(plan => plan.Id);
            var userEmail = User?.Identity?.Name;
            subject.User = _context.Users.First(x => x.Email.Equals(userEmail));
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.Include(x => x.User).SingleAsync(i => i.Id == id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            try
            {
                var changedSubject = await _context.Subjects.Include(x => x.User).FirstAsync(x => x.Id == subject.Id);
                changedSubject.Name = subject.Name;
                _context.Update(changedSubject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(subject.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View(subject);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Plans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subjects== null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Plans == null)
            {
                return Problem("Entity set 'AppDBContext.Plans'  is null.");
            }
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return (_context.Subjects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
