using AutoMapper;
using InvestLink_BLL.Helper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace InvestLink.Controllers
{
    public class AdvertisementController : Controller
    {
        #region Fields



        private readonly IAdvertisement advertisement;
        private readonly IEmployee employee;
        private readonly ILicense license;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;


        #endregion

        //-----------------------------------------
        #region Ctor
        public AdvertisementController(IAdvertisement advertisement,IEmployee employee,ILicense license, IMapper mapper, IToastNotification toastNotification)
        {

            this.advertisement = advertisement;
            this.employee = employee;
            this.license = license;
            this.mapper = mapper;
            this.toastNotification = toastNotification;

        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await advertisement.GetAllAsync();

            var result = mapper.Map<IEnumerable<AdvertisementVM>>(data);
            return View(result);

        }

        public async Task<IActionResult> InvestorIndex()
        {
            var data = await advertisement.GetAllAsync();

            var result = mapper.Map<IEnumerable<AdvertisementVM>>(data);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create(int EmployeeId)
        {

            var model = new AdvertisementVM();
            model.EmployeeId = EmployeeId;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementVM obj)
        {
            try
            {
                var ImageName = FileUpLoader.UploaderFile(obj.Image, "Doc");
                obj.ImageName = ImageName;
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Advertisement>(obj);

                    await advertisement.CreateAsync(data);
                    toastNotification.AddSuccessToastMessage("تم   إنشاء إعلان بنجاح.");
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
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var data = await advertisement.GetByIdAsync(Id);
            var result = mapper.Map<AdvertisementVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdvertisementVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    //1.فحص هل قام المستخدم برفع صورة جديدة؟
                    if (obj.Image != null)
                    {
                        // حالة: المستخدم اختار صورة جديدة
                        // نقوم برفع الصورة وحفظ الاسم الجديد
                        string fileName = FileUpLoader.UploaderFile(obj.Image, "Doc");
                        obj.ImageName = fileName;

                        // (اختياري) يمكنك هنا حذف الصورة القديمة لتوفير المساحة
                    }
                    else
                    {
                        // حالة: المستخدم لم يختر صورة (يريد إبقاء القديمة)
                        // يجب أن نجلب الاسم القديم من قاعدة البيانات حتى لا يتم حفظه كـ null
                        var oldEntity = await advertisement.GetByIdAsync(obj.Id);

                        // نضع الاسم القديم في الكائن الجديد
                        obj.ImageName = oldEntity.ImageName;
                    }
                    var data = mapper.Map<Advertisement>(obj);
                    await advertisement.UpdateAsync(data);
                    toastNotification.AddSuccessToastMessage("تم   تعديل بيانات  بنجاح.");
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
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var data = await advertisement.GetByIdAsync(Id);
            var result = mapper.Map<AdvertisementVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(AdvertisementVM obj)
        {
            try
            {
                var data = mapper.Map<Advertisement>(obj);
                await advertisement.DeleteAsync(data);
                toastNotification.AddSuccessToastMessage("تم   حذف  بنجاح.");
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
            var data = await advertisement.GetByIdAsync(Id);
            var result = mapper.Map<AdvertisementVM>(data);

            return View(result);
        }

    }
}

#endregion




