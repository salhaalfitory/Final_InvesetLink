using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using NToastNotify;
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
        private readonly IProjectCoordinator projectcoordinator;
        private readonly IEmployee employee;
        private readonly IToastNotification toastNotification;
        #endregion

        //-----------------------------------------
        #region Ctor
        public LicenseController(IProject project, IMapper mapper, ILicense license, IProjectCoordinator projectcoordinator, IEmployee employee, IToastNotification toastNotification)
        {
            this.project = project;
            this.mapper = mapper;
            this.license = license;
            this.projectcoordinator = projectcoordinator;
            this.employee = employee;
            this.toastNotification = toastNotification;

        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
           

            return View();
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

            var expiredData = await license.GetExpiredLicensesAsync();

            var result = mapper.Map<IEnumerable<LicenseVM>>(expiredData);
            var employeesData = await employee.GetAllAsync();

            ViewBag.EmployeesList = new SelectList(employeesData, "Id", "Name");
            // 3. إرسال البيانات للصفحة
            return View(result);
        }


        //[HttpPost]
        //public async Task<IActionResult> SendReportRequest(int LicenseId, int EmployeeId)
        //{

        //    // 2. البحث عن الرخصة لجلب رقم المشروع (لأن الجدول يطلب ProjectId)
        //    // نستخدم الـ Repo الخاص بالرخص لجلبها
        //    var licenseData = await license.GetByIdAsync(LicenseId);
        //    InvestLink_DAL.Entities.ProjectCoordinator obj = new InvestLink_DAL.Entities.ProjectCoordinator();
        //    obj.ProjectId = licenseData.ProjectId;
        //    obj.EmployeeId = EmployeeId;
        //    obj.StartDate = DateTime.Now;

        //    await projectcoordinator.CreateAsync(obj);

        //    TempData["Message"] = "تم تعيين الموظف للمشروع بنجاح ✅";
        //    return RedirectToAction("Expiredlicenses");

        //}
        [HttpPost]
        public async Task<IActionResult> SendReportRequest(int licenseId, int employeeId)
        {
            // 1. جلب بيانات الرخصة للحصول على ProjectId
            var licenseData = await license.GetByIdAsync(licenseId);
            if (licenseData == null) return NotFound();

            var projectId = licenseData.ProjectId;

            // 2. التحقق: هل الموظف معين مسبقاً لهذا المشروع؟
            // ملاحظة: افترضنا أن لديك ميثود في الـ Repo للبحث (مثلاً GetAll أو Find)
            var allCoordinators = await projectcoordinator.GetAllAsync(); // أو ميثود تبحث بالشرط مباشرة
            bool isAlreadyAssigned = allCoordinators.Any(pc => pc.EmployeeId == employeeId && pc.ProjectId == projectId);

            if (isAlreadyAssigned)
            {
                toastNotification.AddSuccessToastMessage("هذا الموظف معين بالفعل لهذا المشروع مسبقاً.");
             
                return RedirectToAction("Expiredlicenses");
            }

            // 3. إذا لم يكن موجوداً، نقوم بالإضافة
            InvestLink_DAL.Entities.ProjectCoordinator obj = new InvestLink_DAL.Entities.ProjectCoordinator();
            obj.ProjectId = projectId;
            obj.EmployeeId = employeeId;
            obj.StartDate = DateTime.Now;

            await projectcoordinator.CreateAsync(obj);
            toastNotification.AddSuccessToastMessage("تم تعيين الموظف للمشروع بنجاح.");
            
            return RedirectToAction("Expiredlicenses");
        }
    }
}

#endregion




