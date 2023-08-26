using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Models.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if(!ModelState.IsValid)
                return View(login);

            var user = await _userManager.FindByEmailAsync(login.Email);

            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);

                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }

                login.ErrorMessage = "Senha incorreta";
                return View(login);
            }

            login.ErrorMessage = "Usuário inexistente";
            return View(login);
        }


        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if(!ModelState.IsValid)
                return View(register);

            var user = await _userManager.FindByEmailAsync(register.Email);
            if(user != null)
            {
                register.ErrorMessage = "Usuário já cadastrado";
                return View(register);
            }


            AppUser newUser = new AppUser
            {
                Email = register.Email,
                UserName = register.Email,
            };

            var createdUser = await _userManager.CreateAsync(newUser, register.Password);
            if (createdUser.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            else
            {
                register.ErrorMessage = String.Join("\n", createdUser.Errors.Select(e => e.Description)).TrimStart();
                return View(register);
            }

            return View("RegisterCompleted");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Race");
        }
    }
}
