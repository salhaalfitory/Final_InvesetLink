using InvestLink_BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;


namespace InvestLink.Controllers
{
    public class RoleController : Controller
    {
        #region Fields
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IToastNotification toastNotification;

        #endregion


        #region Ctor

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IToastNotification toastNotification)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.toastNotification = toastNotification;
        }



        #endregion
        #region Actions
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var role = roleManager.Roles;
            return View(role);
        }


        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(RoleVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole()
                    {
                        Name = model.Name

                    };

                    var result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        toastNotification.AddSuccessToastMessage("تم إنشاء بنجاح.");
                        return RedirectToAction("Index");
                       
                    }
                        
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                            toastNotification.AddSuccessToastMessage("يرجى تحقق من بيانات.");
                            return View(model);
                        }
                    }
                }
                TempData["Message"] = "Validation Error";
                toastNotification.AddSuccessToastMessage("يرجى تحقق من بيانات.");
                return View(model);
            }
            catch (Exception ex)
            {
                toastNotification.AddSuccessToastMessage("حدث خطا غير متوقع.");
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {

            var role = await roleManager.FindByIdAsync(Id);
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Update(IdentityRole model)
                    {

            try
            {
                if (ModelState.IsValid)
                {
                    var role = await roleManager.FindByIdAsync(model.Id);

                    role.Name = model.Name;
                    role.NormalizedName = model.Name.ToUpper();



                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {

                        toastNotification.AddSuccessToastMessage("تم تعديل بيانات بنجاح.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                            toastNotification.AddSuccessToastMessage("يرجى تحقق من بيانات.");
                            return View(model);
                        }
                    }
                    }
                TempData["Message"] = "Validation Error";
                toastNotification.AddSuccessToastMessage("يرجى تحقق من بيانات.");
                return View(model);
            }
            catch (Exception ex)
            {
                toastNotification.AddSuccessToastMessage("حدث خطا غير متوقع.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {

            var role = await roleManager.FindByIdAsync(Id);
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole model)
        {
            try
            {

                var role = await roleManager.FindByIdAsync(model.Id);

                if (role != null)
                {

                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {

                        toastNotification.AddSuccessToastMessage("تم حذف بنجاح.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                            toastNotification.AddSuccessToastMessage("يرجى تحقق من بيانات.");
                            return View(model);
                        }
                        return View(model);
                    }
                }

                TempData["Message"] = "Validation Error";
                toastNotification.AddSuccessToastMessage("يرجى تحقق من بيانات.");
                return View(model);
            }
            catch (Exception ex)
            {
                toastNotification.AddSuccessToastMessage("حدث خطا غير متوقع.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemove(string RoleId)
        {
            ViewBag.RoleId = RoleId;
            var role = await roleManager.FindByIdAsync(RoleId);
            ViewBag.Name = role.Name;
            var model = new List<UserInRoleVM>();//Id UserName IsSelected 
            //get all users
            var users = userManager.Users;
            foreach (var user in users)
            {
                var UserInRole = new UserInRoleVM()//map
                {
                    Id = user.Id,
                    UserName = user.UserName,
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    UserInRole.IsSelected = true;
                }
                else
                    UserInRole.IsSelected = false;
                model.Add(UserInRole);
            }
            toastNotification.AddSuccessToastMessage("تم تغيير صلاحيه بنجاح.");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemove(List<UserInRoleVM> model, string RoleId)
        {

            var role = await roleManager.FindByIdAsync(RoleId);
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].Id);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
        }
                else
                {
                    continue;
    }
}
            toastNotification.AddSuccessToastMessage("تم تغيير صلاحيه بنجاح.");
            return RedirectToAction("Update", new { id = RoleId });
        }
    }
}
#endregion