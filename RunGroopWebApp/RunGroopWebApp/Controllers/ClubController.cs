using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private AppDbContext _context { get; }

        public ClubController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var clubs = _context.Clubs.Include(c => c.Address).ToList();
            return View(clubs);
        }

        public IActionResult Detail(int id)
        {
            var club = _context.Clubs.Include(c => c.Address).FirstOrDefault(x => x.Id == id);

            return View(club);
        }
    }
}
