using Microsoft.AspNetCore.Mvc;

namespace WebApplication2_NoteApp.Controllers
{
    public class NoteController : Controller
    {
        public int _userId = 0;

        public IActionResult Index()
        {
            if(_userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}
