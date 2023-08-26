using Microsoft.AspNetCore.Mvc;

namespace RunGroopWebApp.Controllers
{
    public class ApplicationErrorController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View("403");
        }
    }
}
