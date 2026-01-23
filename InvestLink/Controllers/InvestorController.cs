using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using InvestLink_DAL.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace InvestLink.Controllers
{
    public class InvestorController : Controller
    {
        #region Fields


       
        private readonly IMapper mapper;
        private readonly IInvestor investor;
        private readonly INationality nationality;
        private readonly IToastNotification toastNotification;

        #endregion

        //-----------------------------------------
        #region Ctor
        public InvestorController( IMapper mapper, IInvestor investor, INationality nationality, IToastNotification toastNotification)
        {
           
            this.mapper = mapper;
            this.investor = investor;
            this.nationality = nationality;
            this.toastNotification = toastNotification;

        }

        #endregion
        //--------------------------------------------------

        #region Actions

        //[Authorize(Roles = "Investor")]
        public async Task<IActionResult> Index()
        {
            var data = await investor.GetAllAsync();

            var result = mapper.Map<IEnumerable<InvestorVM>>(data);
            return View(result);
        }

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
                    toastNotification.AddSuccessToastMessage("تم  إنشاء  بنجاح.");
                    return RedirectToAction("Index", "Project");
                }
                TempData["Meesage"] = "validation Error";
                toastNotification.AddErrorToastMessage(" يرجى التحقق من البيانات");
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                toastNotification.AddErrorToastMessage("حدث خطأ غير متوقع.");
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
                    toastNotification.AddSuccessToastMessage("تم تعديل بيانات بنجاح.");
                    return RedirectToAction("Index");
                }
                TempData["Meesage"] = "validation Error";
                toastNotification.AddErrorToastMessage(" يرجى التحقق من البيانات");
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                toastNotification.AddErrorToastMessage("حدث خطأ غير متوقع.");
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
                toastNotification.AddSuccessToastMessage("تم حذف بنجاح.");
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                toastNotification.AddErrorToastMessage("حدث خطأ غير متوقع.");

                return View(obj);
            }
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



