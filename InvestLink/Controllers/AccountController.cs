using Azure.Core;
using InvestLink_BLL.Helper;
using InvestLink_BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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

                    

                        return RedirectToAction("Index","Home");
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

                        // توليد الرمز
                        Random generator = new Random();
                        string otp = generator.Next(0, 1000000).ToString("D6");
                        string expiryTime = DateTime.Now.AddMinutes(15).ToString();

                        // حفظ الرمز
                        await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("OTP_Code", otp));
                        await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("OTP_Expiry", expiryTime));

                        // -----------------------------------------------------------
                        // كود إرسال الإيميل
                        // -----------------------------------------------------------
                        try
                        {
                            var mailInfo = new MailVM
                            {
                                Email = user.Email,
                                Title = "تفعيل حساب InvestLink",
                                Message = $"مرحباً {user.UserName}،\nرمز التفعيل الخاص بك هو: {otp}"
                            };

                            MailSender.SendMail(mailInfo);
                        }
                        catch (Exception ex)
                        {
                            // طباعة الخطأ فقط دون إيقاف البرنامج
                            System.Diagnostics.Debug.WriteLine("فشل الإرسال: " + ex.Message);
                        }
                        // -----------------------------------------------------------

                        toastNotification.AddSuccessToastMessage("تم إنشاء الحساب، تفقد بريدك.");
                        return RedirectToAction("VerifyEmail", new { email = user.Email });
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        toastNotification.AddErrorToastMessage("فشل إنشاء حساب.");
                    }
                }
                // إذا كان الموديل غير صالح
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                toastNotification.AddErrorToastMessage("حدث خطأ غير متوقع.");
                return View(model);
            }
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
                    toastNotification.AddErrorToastMessage(" اذا كان البريد مسجلاً لدينا، فقد تم إرسال رابط التعيين إليه.");
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
        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            // التحقق من  الرابط
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
               
                return RedirectToAction("Login");
            }

            // نرسل بيانات الى فيو ليتم ارسالها
            var model = new ResetPasswordVM
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
               
                return RedirectToAction("Login");
            }

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                toastNotification.AddSuccessToastMessage("تم تغيير كلمة المرور بنجاح.");
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            toastNotification.AddSuccessToastMessage("تم تسجيل خروج.");
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult VerifyEmail(string email)
        {
            // إذا كان الإيميل فارغاً، نرجعه للتسجيل
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Regestration");
            }

            // --- هذا الجزء المفقود أو الخطأ عندك ---
            // يجب تجهيز الموديل وإرساله للصفحة
            var model = new VerifyEmailVM
            {
                Email = email
            };

            // لاحظ أننا نمرر كلمة model داخل القوسين
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(string email, string code)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return RedirectToAction("Register");

            // 1. جلب كل الـ Claims الخاصة بالمستخدم
            var claims = await userManager.GetClaimsAsync(user);

            // 2. استخراج الرمز ووقت الانتهاء
            var storedToken = claims.FirstOrDefault(c => c.Type == "OTP_Code")?.Value;
            var expiryString = claims.FirstOrDefault(c => c.Type == "OTP_Expiry")?.Value;

            // 3. التحقق
            if (storedToken != null && expiryString != null)
            {
                DateTime expiryDate = DateTime.Parse(expiryString);

                if (storedToken == code && expiryDate > DateTime.Now)
                {
                    // الرمز صحيح والوقت ساري

                    // تفعيل الإيميل (هذه خاصية موجودة أصلاً في IdentityUser)
                    user.EmailConfirmed = true;
                    await userManager.UpdateAsync(user);

                    // (اختياري) تنظيف الـ Claims القديمة لأننا لم نعد نحتاجها
                    await userManager.RemoveClaimAsync(user, claims.FirstOrDefault(c => c.Type == "OTP_Code"));
                    await userManager.RemoveClaimAsync(user, claims.FirstOrDefault(c => c.Type == "OTP_Expiry"));

                    // تسجيل الدخول
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "الرمز غير صحيح أو منتهي";
            ViewBag.Email = email;
            return View();
        }

    }
}