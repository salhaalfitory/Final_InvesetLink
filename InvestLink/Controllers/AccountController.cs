using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace InvestLink.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
     
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

            [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try { 
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Account invalid");
                }
                return View(model);
            }
                ModelState.AddModelError("", "Account invalid");
                return View(model);

            }
            catch (Exception ex)
            {
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Regestration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Regestration(RegestrationVM model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var user = new IdentityUser
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                    };
                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Investor");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(model);
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

    }
}
