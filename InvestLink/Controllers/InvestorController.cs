using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvestLink.Controllers
{
    public class InvestorController : Controller
    {
        #region Fields


       
        private readonly IMapper mapper;
        private readonly IInvestor investor;
        private readonly INationality nationality;


        #endregion

        //-----------------------------------------
        #region Ctor
        public InvestorController( IMapper mapper, IInvestor investor, INationality nationality)
        {
           
            this.mapper = mapper;
            this.investor = investor;
            this.nationality = nationality;


        }

        #endregion
        //--------------------------------------------------

        #region Actions

        [Authorize(Roles = "Investor")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var nat = await nationality.GetAllAsync();
            ViewBag.NationalityList = new SelectList(nat, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InvestorVM obj)
            {
            try
            {
                
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
        public async Task<IActionResult> Update(int Id)
        {
            var data = await investor.GetByIdAsync(Id);
            var result = mapper.Map<InvestorVM>(data);
            var nationalities = await nationality.GetAllAsync();
            ViewBag.NationalityList = new SelectList(nationalities, "Id", "Name", result.NationalityId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(InvestorVM obj)
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
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var data = await investor.GetByIdAsync(Id);
            var result = mapper.Map<InvestorVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(InvestorVM obj)
        {
            try
            {
                var data = mapper.Map<Investor>(obj);
                await investor.DeleteAsync(data);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;

                return View(obj);
            }
        }
        public IActionResult Save()
        {
            return View();
        }
        public async Task<IActionResult> Details(int Id)
        {
            var data = await investor.GetByIdAsync(Id);
            var result = mapper.Map<InvestorVM>(data);

            return View(result);
        }

    }
}

#endregion



