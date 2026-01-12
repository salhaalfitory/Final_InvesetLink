using InvestLink_BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InvestLink.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        //private readonly SignInManager<IdentityUser> signInManager;


        public RoleController(RoleManager<IdentityRole> roleManager) {

            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleVM model){
            try
            {

                if (ModelState.IsValid == true)
                {
                    var role = new IdentityRole
                    {
                        Name = model.Name,
                    };

                    var result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach(var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(model);
                    }
                }
                TempData["Meesage"] = "validation Error";
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }

        }
    }
}
