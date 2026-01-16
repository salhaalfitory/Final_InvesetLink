using InvestLink_BLL.Helper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace InvestLink.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IToastNotification toastNotification;

        private readonly IInvestor investor;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, IToastNotification toastNotification, IInvestor investor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            toastNotification = toastNotification;
            this.investor = investor;
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

                    //var user1 = new Investor
                    //{
                    //    Name = model.UserName,
                    //    Email = model.Email,
                    //};
                    var result = await userManager.CreateAsync(user, model.Password);

                    //await investor.CreateAsync(data);

                    //var resu = await investor.CreateAsync(user1);


                    if (result.Succeeded)
                    {
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

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { model.Email, token }, Request.Scheme);
                        MailSender.SendMail(new MailVM() { Email = model.Email, Title = "Reset Password", Message = passwordResetLink });
                        toastNotification.AddSuccessToastMessage("تم إرسال رابط إعادة تعيين كلمة المرور إلى بريدك الإلكتروني.");
                        return RedirectToAction("ConfirmForgetPassword");
                    }
                    toastNotification.AddErrorToastMessage("البريد الإلكتروني غير موجود.");
                    return View(model);
                }
                toastNotification.AddErrorToastMessage("صيغة البريد الإلكتروني غير صحيحة.");
                return View(model);
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage("حدث خطأ أثناء إرسال رابط إعادة تعيين كلمة المرور.");
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
