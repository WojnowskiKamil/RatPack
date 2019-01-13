using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Rat_Pack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rat_Pack.Controllers
{
    public class AccountController : Controller
    {
        protected UserManager<IdentityUser> UserManager { get; }

        protected SignInManager<IdentityUser, string> SignInManager { get; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser, string> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(model.Login)
                {
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result == IdentityResult.Success)
                {
                    if (model.IsClientSender==true)
                    {
                        UserManager.AddToRole(user.Id, "Kierowca");
                        await SignInManager.SignInAsync(user, false, false);
                        return RedirectToAction("Index", "Packages");
                    }
                    else
                    {
                        UserManager.AddToRole(user.Id, "Klient");
                        await SignInManager.SignInAsync(user, false, false);
                        return RedirectToAction("Index", "Packages");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result == SignInStatus.Success)
                {
                    if (User.IsInRole("Kierowca"))
                    {
                        return RedirectToAction("Index2", "Packages");
                    }
                    return RedirectToAction("Index", "Packages");
                }
                else
                {
                    ModelState.AddModelError("", "Podano zły login lub hasło");
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            SignInManager.AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}