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
            if (ModelState.IsValid)
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
                    HttpContext.Session.SetInt32("userid", user.Id);
                    HttpContext.Session.SetString("username", user.Username);

                    return RedirectToAction("Index", "Note");
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı kullanıcı adı ya da şifre!");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login");
            }

            UserManager userManager = new UserManager();
            User user = userManager.GetUserById(userid.Value);

            ProfileModel model = new ProfileModel();
            model.Name = user.Name;
            model.Surname = user.Surname;

            return View(model);
        }

        [HttpPost]
        public IActionResult Profile(ProfileModel model)
        {
            int? userid = HttpContext.Session.GetInt32("userid");

            if (userid == null)
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                bool done = false;

                if (model.IsUpdatePassword)
                {
                    done = userManager.UpdatePassword(userid.Value, model.Password);
                }
                else
                {
                    done = userManager.UpdateProfile(userid.Value, model.Name, model.Surname);
                }

                //if (done == false)
                if (!done)
                {
                    ModelState.AddModelError("", "İşlem yapılamadı. Bir hata oluştu.");
                }
                else
                {
                    ViewData["ok"] = "Profil güncellendi.";
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
