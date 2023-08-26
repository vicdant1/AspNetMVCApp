using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly AppDbContext _context;

        public RaceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var races = _context.Races.Include(r => r.Address).ToList();
            return View(races);
        }


        public IActionResult Detail(int id)
        {
            var race = _context.Races.Include(r => r.Address).FirstOrDefault(r => r.Id == id);
            return View(race);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if(!ModelState.IsValid)
            {
                return View(race);
            }

            _context.Races.Add(race);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
