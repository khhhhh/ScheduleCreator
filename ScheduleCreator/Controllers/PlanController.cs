using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulePlan.DataAccess;
using SchedulePlan.Models;
using System.Security.Claims;

namespace ScheduleCreator.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly AppDBContext _context;

        public PlanController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currUser = _context.Users.First(x => x.Email.Equals(User.Identity.Name));
            var plans = _context.Plans.Where(plan => plan.User.Equals(currUser));
            return View(plans);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var newPlan = new Plan();
            var userEmail = User?.Identity?.Name;
            newPlan.User = _context.Users.First(x => x.Email.Equals(userEmail));
            return View(newPlan);
        }
        [HttpPost]
        public IActionResult Create(Plan plan)
        {
            //var planId = _context.Plans.Max(plan => plan.Id);
            var userEmail = User?.Identity?.Name;
            plan.User = _context.Users.First(x => x.Email.Equals(userEmail));
            _context.Plans.Add(plan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
