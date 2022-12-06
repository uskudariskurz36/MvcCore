using CheckPasswordStrength;
using Microsoft.AspNetCore.Mvc;
using WebApplication2_NoteApp.Helpers;
using WebApplication2_NoteApp.Models;

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
            bool valid = true;

            if (string.IsNullOrEmpty(username))
            {
                ViewData["err-username"] = "Username is required.";
                valid = false;
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                ViewData["err-password"] = "Password is required.";
                valid = false;
            }
            else
            {
                StrengthProperty checkPass = ClassifyStrength.PasswordStrength(password);
                ViewData["err-password-strength-text"] = checkPass.Value;
                ViewData["err-password-strength"] = checkPass.Id;

                if (checkPass.Id == 0)
                {
                    valid = false;
                }
            }

            if (string.IsNullOrEmpty(repassword))
            {
                ViewData["err-repassword"] = "Re-Password is required.";
                valid = false;
            }

            if (password != repassword)
            {
                ViewData["err-repassword"] = "Password and re-password does not match.";
                valid = false;
            }

            if (valid)
            {
                UserManager userManager = new UserManager();
                bool done = userManager.AddUser(username, password);

                ViewData["done"] = done;
            }

            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // kullanıcı uname ve şifre kontrol
            }

            return View(model);
        }
    }
}
