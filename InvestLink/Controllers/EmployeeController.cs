using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace InvestLink.Controllers
{
    public class EmployeeController : Controller
    {

        #region Fields


        private readonly IEmployee employee;
        private readonly INationality nationality;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;


        #endregion

        //-----------------------------------------
        #region Ctor
        public EmployeeController(IEmployee employee, INationality nationality, IMapper mapper, IToastNotification toastNotification)
        {
            this.employee = employee;
            this.nationality = nationality;
            this.mapper = mapper;
            this.toastNotification = toastNotification;

        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await employee.GetAllAsync();

            var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
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
        public async Task<IActionResult> Create(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Employee>(obj);

                    await employee.CreateAsync(data);
                    toastNotification.AddSuccessToastMessage("تم  إنشاء  بنجاح.");
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "validation Error";
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
            var data = await employee.GetByIdAsync(Id);
            var nat = await nationality.GetAllAsync();
            ViewBag.NationalityList = new SelectList(nat, "Id", "Name");
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Employee>(obj);
                    await employee.UpdateAsync(data);
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
                ModelState.AddModelError("", ex.Message);
                toastNotification.AddErrorToastMessage("حدث خطأ غير متوقع.");
                return View(obj);
            }
        }
  
       
      
    }
}

#endregion



