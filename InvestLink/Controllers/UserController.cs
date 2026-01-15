using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_BLL.Repository;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace InvestLink.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<UserController> logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly INationality nationality;
        private readonly IToastNotification toastNotification;
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UserController> logger,
            RoleManager<IdentityRole> roleManager, INationality nationality, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.roleManager = roleManager;
            this.nationality = nationality;
            this.toastNotification = toastNotification;
        }

        //-----------------------------------------
  
        public IActionResult Index()
        {
            var data = userManager.Users;
            return View(data);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //await PopulateRolesAsync(new EmployeeVM());

            var nat = await nationality.GetAllAsync();
            ViewBag.NationalityList = new SelectList(nat, "Id", "Name");
            return View(new EmployeeVM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                   UserName = model.Name,
                    Email = model.Email,
                
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        await userManager.AddToRoleAsync(user, model.Role);
                    }

                    logger.LogInformation("User {Email} created successfully by {AdminUser}", model.Email, User.Identity!.Name);
                   
                    toastNotification.AddSuccessToastMessage("تم إنشاء موظف  بنجاح  .");
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            await PopulateRolesAsync(model);
            var nat = await nationality.GetAllAsync(); // أو أي دالة تجلب البيانات عندك
            ViewBag.NationalityList = new SelectList(nat, "Id", "Name");
            return View(model);
        }
        private async Task PopulateRolesAsync(EmployeeVM model)
        {
            // جلب الأدوار وتحويلها إلى قائمة يفهمها الـ DropdownList
            var roles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToList();

            ViewBag.Roles = roles;
        }

        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            return View(user);
        }
       [HttpPost]
        public async Task<IActionResult> Update(IdentityUser model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.NormalizedUserName = model.UserName.ToUpper();
            user.NormalizedEmail = model.Email.ToUpper();
            user.SecurityStamp = model.SecurityStamp;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                toastNotification.AddSuccessToastMessage("تم تعديل بيانات بنجاح  .");
                return RedirectToAction("Index");
            }
            else {                
                foreach (var error in result.Errors)
                {

                    ModelState.AddModelError("", error.Description);
                }
                return View(model);

            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdentityUser model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            var result = await userManager.DeleteAsync(user);

            //try {

            if (result.Succeeded)
            {
                toastNotification.AddSuccessToastMessage("تم حذف بنجاح  .");
                return RedirectToAction("Index");
            }
            else {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);

            }
        }
    
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //    }
        //   return View(model);
        //}
      
    }
}
