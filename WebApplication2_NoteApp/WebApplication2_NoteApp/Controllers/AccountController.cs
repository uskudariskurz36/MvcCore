using CheckPasswordStrength;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                StrengthProperty checkPass = ClassifyStrength.PasswordStrength(model.Password);
                ViewData["err-password-strength-text"] = checkPass.Value;
                ViewData["err-password-strength"] = checkPass.Id;

                if (checkPass.Id == 0)  // şifre weak (zayıf) ise..
                {
                    ModelState.AddModelError("Password", "Şifre uygun değildir.");
                }
            }

            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                bool done = userManager.AddUser(model.Username, model.Password);

                ViewData["done"] = done;
            }

            return View(model);
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
                UserManager userManager = new UserManager();
                User user = userManager.Authenticate(model.Username, model.Password);

                if (user != null)
                {
                    NoteController noteController = new NoteController();
                    noteController._userId = user.Id;

                    return RedirectToAction("Index", "Note");
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı kullanıcı adı ya da şifre!");
                }
            }

            return View(model);
        }
    }
}
