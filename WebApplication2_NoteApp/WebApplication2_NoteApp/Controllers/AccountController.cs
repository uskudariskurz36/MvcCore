using Microsoft.AspNetCore.Mvc;

namespace WebApplication2_NoteApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string repassword)
        {
            if (string.IsNullOrEmpty(username))
            {
                ViewData["err-username"] = "Username is required.";
            }

            if (string.IsNullOrEmpty(password))
            {
                ViewData["err-password"] = "Password is required.";
            }

            if (string.IsNullOrEmpty(repassword))
            {
                ViewData["err-repassword"] = "Re-Password is required.";
            }

            if (password != repassword)
            {
                ViewData["err-repassword"] = "Password and re-password does not match.";
            }


            return View();
        }
    }
}
