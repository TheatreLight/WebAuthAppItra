using Microsoft.AspNetCore.Mvc;

namespace WebAuthAppItra.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Users() 
        {
            return View();
        }
    }
}
