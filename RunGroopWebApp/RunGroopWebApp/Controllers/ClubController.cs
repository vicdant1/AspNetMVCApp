using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Models.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private AppDbContext _context { get; }
        private readonly UserManager<AppUser> _userManager;

        public ClubController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        {
            if (!ModelState.IsValid)
            {
                return View(club);
            }

            var currentUserId = _userManager.GetUserId(User);
            club.AppUserId = currentUserId;

            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = _context.Clubs.Where(c => c.Id == id).FirstOrDefault();
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                Url = club.Image,
                Category = club.ClubCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubvm)
        {


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Falha ao editar clube");
                return View("Edit", clubvm);
            }

            var club = new Club
            {
                Id = id,
                Title = clubvm.Title,
                Description = clubvm.Description,
                AddressId = clubvm.AddressId,
                Address = clubvm.Address,
            };

            _context.Clubs.Update(club);

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }
    }
}
