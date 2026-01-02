using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.ComponentModel;
using License = InvestLink_DAL.Entities.License;



namespace InvestLink.Controllers
{
    public class LicenseController : Controller
    {
        #region Fields



        private readonly IProject project;
        private readonly IMapper mapper;
        private readonly ILicense license;


        #endregion

        //-----------------------------------------
        #region Ctor
        public LicenseController(IProject project, IMapper mapper, ILicense license)
        {
            this.project = project;
            this.mapper = mapper;
            this.license = license;


        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await license.GetAllAsync();
            var result = mapper.Map<IEnumerable<LicenseVM>>(data);

            return View(result);
        }
      

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LicenseVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<License>(obj);

                    await license.CreateAsync(data);
                    return RedirectToAction("Index");
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
        //[HttpGet]
        //public async Task<IActionResult> Update(int Id)
        //{
        //    // 1. جلب البيانات
        //    var data = await license.GetByIdAsync(Id);

        //    // 2. التحقق: إذا كانت البيانات غير موجودة (null)
        //    if (data == null)
        //    {
        //        // إما أن تظهر صفحة "غير موجود"
        //        // return NotFound(); 

        //        // أو تعيد المستخدم للصفحة الرئيسية مع رسالة خطأ (أفضل للمستخدم)
        //        TempData["Message"] = "عفواً، هذا السجل غير موجود.";
        //        return RedirectToAction("Index");
        //    }

        //    // 3. التحويل والإرسال في حال وجود بيانات
        //    var result = mapper.Map<LicenseVM>(data);
        //    return View(result);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var data = await license.GetByIdAsync(Id);

        //    // نفس التحقق هنا ضروري جداً لتجنب الخطأ في صفحة الحذف
        //    if (data == null)
        //    {
        //        TempData["Message"] = "عفواً، هذا السجل غير موجود.";
        //        return RedirectToAction("Index");
        //    }

        //    var result = mapper.Map<LicenseVM>(data);
        //    return View(result);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var data = await license.GetByIdAsync(Id);
        //    var result = mapper.Map<LicenseVM>(data);
        //    return View(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(LicenseVM obj)
        {
            try
            {
                var data = mapper.Map<License>(obj);
                await license.DeleteAsync(data);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;

                return View(obj);
            }
        }


        public async Task<IActionResult> Details(int Id)
        {
            var data = await license.GetByProjectIdAsync(Id);
            if (data == null)
            {
                TempData["Message"] = "الرخصة غير موجودة";
                return RedirectToAction("Index");
            }
            var result = mapper.Map<LicenseVM>(data);

            return View(result);
        }
        public async Task<IActionResult> Expiredlicenses()
        {

            // 1. استدعاء الدالة الجديدة من السيرفس/الريبو
            var expiredData = await license.GetExpiredLicensesAsync();

            // 2. التحويل إلى LicenseVM (تأكدي أنك تحولين لـ LicenseVM مش ProjectVM)
            var result = mapper.Map<IEnumerable<LicenseVM>>(expiredData);
            var employeesData = await project.GetAllAsync();

            ViewBag.EmployeesList = new SelectList(employeesData, "EmployeeId", "Name");
            // 3. إرسال البيانات للصفحة
            return View(result);
        }

    }
}

#endregion




