using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace InvestLink.Controllers
{
    public class NationalityController : Controller
    {
        #region Fields


        private readonly INationality nationality;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;



        #endregion

        //-----------------------------------------
        #region Ctor
        public NationalityController(INationality nationality, IMapper mapper, IToastNotification toastNotification)
        {
            this.nationality = nationality;
            this.mapper = mapper; 
            this.toastNotification = toastNotification;



        }

        #endregion
        //--------------------------------------------------

        #region Actions
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var data = await nationality.GetAllAsync();

            var result = mapper.Map<IEnumerable<NationalityVM>>(data);
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
           

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NationalityVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Nationality>(obj);

                    await nationality.CreateAsync(data);
                    toastNotification.AddSuccessToastMessage("تم إنشاء جنسية بنجاح.");
                    return RedirectToAction("Index");
                }
                toastNotification.AddSuccessToastMessage("فشل إنشاء جنسية، يرجى التحقق من البيانات.");
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
            var data = await nationality.GetByIdAsync(Id);
            var result = mapper.Map<NationalityVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(NationalityVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Nationality>(obj);
                    await nationality.UpdateAsync(data);
                    toastNotification.AddSuccessToastMessage("تم تعديل بيانات بنجاح.");
                    return RedirectToAction("Index");
                }
                toastNotification.AddSuccessToastMessage("فشل تعديل بيانات، يرجى التحقق من البيانات.");
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
            var data = await nationality.GetByIdAsync(Id);
            var result = mapper.Map<NationalityVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(NationalityVM obj)
        {
            try
            {
                var data = mapper.Map<Nationality>(obj);
                await nationality.DeleteAsync(data);
                toastNotification.AddSuccessToastMessage("تم حذف بنجاح.");
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;

                return View(obj);
            }
        }
      
       

    }
}

#endregion



