using Microsoft.AspNetCore.Mvc;

namespace WebApplication2_NoteApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
