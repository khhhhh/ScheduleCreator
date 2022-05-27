using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchedulePlan.DataAccess;
using SchedulePlan.Models;

namespace ScheduleCreator.Controllers
{
    public class LessonsController : Controller
    {
        private readonly AppDBContext _context;

        public LessonsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index(int? id)
        {
            //var plan = _context.Plans.FirstOrDefault(x => x.Id == id);
            var lessons = _context
                    .Lessons
                    .Include(x=>x.Plan)
                    .Where(x => x.PlanId == id)
                    .OrderBy(x => x.Day)
                    .ThenBy(x => x.StartTime.TimeOfDay);
            ViewBag.LinkableId = id;
            return lessons != null ?
                        View(await lessons.ToListAsync()) :
                        Problem("Entity set 'AppDBContext.Lessons'  is null.");
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.Include(x => x.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create(int? id)
        {
            ViewBag.Plan = _context.Plans.First(x => x.Id == id);
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Lessons/Create/{planid}")]
        public async Task<IActionResult> Create([Bind("Id,Name,Plan,Day,StartTime,EndTime")] Lesson lesson, int? planid)
        {
            lesson.PlanId = planid.Value; 
            //var plan = _context.Plans.First(x => x.Id == planid);
            //lesson.Plan = plan;
            _context.Add(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = planid });
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = _context.Lessons.Include(x => x.Plan).First(x => x.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanId,Name,Plan,Day,StartTime,EndTime")] Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(lesson);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(lesson.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index), new { id = lesson.PlanId});
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lessons == null)
            {
                return Problem("Entity set 'AppDBContext.Lessons'  is null.");
            }
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = lesson.PlanId});
        }

        private bool LessonExists(int id)
        {
            return (_context.Lessons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
