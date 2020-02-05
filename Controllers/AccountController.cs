using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GymSite.Models;
using GymSite.Models.Views;
using GymSite.Relations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GymSite.Controllers
{
    public class AccountController : Controller
    {
        private MyDBContext Context;

        public AccountController(MyDBContext context)
        {
            Context = context;
        }

        private void Authenticate(string userName)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(Claims, "ApplicationsCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id)).GetAwaiter();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterView model)
        {
            if (ModelState.IsValid)
            {
                Staff user = Context.Staffs.GetList.FirstOrDefault(x => x.Login == model.Login);
                if (user == null)
                {
                    // register new staff
                    Context.Staffs.Update(Context.Staffs.ParseToInstert(
                        new Staff()
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            ID = 0,
                            IsAdmin = 0,
                            Login = model.Login,
                            Password = Hashing.GetHash(model.Password)
                        }));
                    Authenticate(model.Login);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or pass");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginView model)
        {
            if (ModelState.IsValid)
            {
                Staff user = Context.Staffs.GetList.FirstOrDefault(x =>
                    x.Login == model.Login && x.Password == Hashing.GetHash(model.Password));
                if (user != null)
                {
                    Authenticate(model.Login);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("","Wrong login or pass");
            }

            return View(model);
        }
    }
}