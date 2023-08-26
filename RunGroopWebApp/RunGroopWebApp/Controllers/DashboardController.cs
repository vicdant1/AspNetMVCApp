using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Models.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        // should have built a complete user service in order to not mix responsabilities around app
        public DashboardController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var currentUserId = _userManager.GetUserId(User);
            var usersClubs = await _context.Clubs.Where(c => c.AppUserId == currentUserId).ToListAsync();
            var usersRaces = await _context.Races.Where(c => c.AppUserId == currentUserId).ToListAsync();


            var dashVm = new DashboardViewModel
            {
                Clubs = usersClubs,
                Races = usersRaces,
            };

            return View(dashVm);
        }
    }
}
