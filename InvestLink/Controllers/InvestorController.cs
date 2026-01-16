using AutoMapper;
using InvestLink_BLL.Helper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvestLink.Controllers
{
    public class InvestorController : Controller
    {
        #region Fields

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        private readonly IMapper mapper;
        private readonly IInvestor investor;
        private readonly INationality nationality;
        #endregion

        //-----------------------------------------
        #region Ctor
        public InvestorController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper, IInvestor investor, INationality nationality)
        {
           
            this.mapper = mapper;
            this.investor = investor;
            this.nationality = nationality;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        #endregion
        //--------------------------------------------------

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Create(int Id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InvestorVM obj)
            {
            try
            {
                obj.PassportName = "";
                obj.CaredName = "";
                obj.CaredNumber = "hi";
                obj.PassportNumber = "12345678";
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Investor>(obj);
                    await investor.CreateAsync(data);
                    return RedirectToAction("Index", "Home");
                }
                TempData["Meesage"] = "validation Error";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(obj);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Profile(int Id)
        {
            var data = await investor.GetByIdAsync(Id);

            var nat = await nationality.GetAllAsync();
            ViewBag.NationalityList = new SelectList(nat, "Id", "Name");
            var result = mapper.Map<InvestorVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(InvestorVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Investor>(obj);
                    await investor.UpdateAsync(data);
                    return RedirectToAction("Index", "Home");
                }
                TempData["Meesage"] = "validation Error";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(obj);
            }
        }
    }
        //----------------------------------------
}


#endregion



