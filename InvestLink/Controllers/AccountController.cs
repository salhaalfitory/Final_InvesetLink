using Azure.Core;
using InvestLink_BLL.Helper;
using InvestLink_BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Policy;





namespace InvestLink.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IToastNotification toastNotification;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.toastNotification = toastNotification;
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        toastNotification.AddSuccessToastMessage("تم تسجيل دخول بنجاح.");
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                        toastNotification.AddErrorToastMessage("فشل تسجيل الدخول، يرجى التحقق من البيانات.");
                        ModelState.AddModelError("", "Account invalid");
                }
                return View(model);
            }
                else { 
                    //ModelState.AddModelError("", "Account invalid");
                    toastNotification.AddErrorToastMessage("المستخدم غير مسجل");
                    return View(model);
                }
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
                        toastNotification.AddSuccessToastMessage("تم إنشاء حساب بنجاح.");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            //toastNotification.AddErrorToastMessage("فشل إنشاء حساب ، يرجى التحقق من البيانات.");
                            ModelState.AddModelError("", item.Description);
                            //toastNotification.AddErrorToastMessage("فشل إنشاء حساب ، يرجى التحقق من البيانات.");

                        }
                        toastNotification.AddErrorToastMessage("فشل إنشاء حساب ، يرجى التحقق من البيانات.");
                        //return View(model);
                    }
                    toastNotification.AddErrorToastMessage("فشل إنشاء حساب ، يرجى التحقق من البيانات.");
                    return View(model);
                }
            }

            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                toastNotification.AddErrorToastMessage("فشل إنشاء حساب ، يرجى التحقق من البيانات.");

            }
            toastNotification.AddErrorToastMessage("فشل إنشاء حساب ، يرجى التحقق من البيانات.");
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
            toastNotification.AddSuccessToastMessage("تم تسجيل خروج.");
            return RedirectToAction("Login", "Account");
        }

    }
}