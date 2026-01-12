using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        //-----------------------------------------
  
        public IActionResult Index()
        {
            var data = userManager.Users;
            return View(data);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
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
