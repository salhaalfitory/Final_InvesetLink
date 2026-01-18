using AutoMapper;
using InvestLink_BLL.Helper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_BLL.Repository;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using NToastNotify;
using Project = InvestLink_DAL.Entities.Project;

namespace InvestLink.Controllers
{
    public class CoordinatorReportController : Controller
    {
        #region Fields


        private readonly ICoordinatorReport coordinatorReport;
        private readonly ILicense license;
        private readonly IProject project;
        private readonly IProjectCoordinator projectcoordinator;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;



        #endregion

        //-----------------------------------------
        #region Ctor
        public CoordinatorReportController(ICoordinatorReport coordinatorReport,ILicense license,IProject project, IProjectCoordinator projectcoordinator, IMapper mapper, IToastNotification toastNotification)
        {
            this.coordinatorReport = coordinatorReport;
            this.license = license;
            this.project = project;
            this.projectcoordinator = projectcoordinator;
            this.mapper = mapper; 
                   this.toastNotification = toastNotification;
        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await coordinatorReport.GetAllAsync();

            var result = mapper.Map<IEnumerable<CoordinatorReportVM>>(data);
            return View(result);
        }

        public async Task<IActionResult> ReCreateLicense(int projectCoordinatorId)
        {
            var _coordinatorReport = await coordinatorReport.GetByIdAsync(projectCoordinatorId);
            var _projectCortiotor = await projectcoordinator.GetByIdAsync(projectCoordinatorId);
            var _project = await project.GetByIdAsync(_projectCortiotor.ProjectId);
            License obj = new License();
            obj.ProjectId = _project.Id;
            obj.ExpireDate = DateTime.Now.AddMinutes(2);
            obj.Type = "مزاولة";
            obj.CreatedDate = DateTime.Now;
            obj.LicenseNumber = $"{DateTime.Now.Year}-LIC";

            await license.CreateAsync(obj);
            toastNotification.AddSuccessToastMessage("تم  تجديد رخصه بنجاح.");
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> RejectedLicenses()
        {

            var data = await project.GetByStateAsync("سحب الترخيص");
            var result = mapper.Map<IEnumerable<ProjectVM>>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> RejectedLicenses(int projectCoordinatorId)
        {

            var _coordinatorReport = await coordinatorReport.GetByIdAsync(projectCoordinatorId);

            var _projectCortiotor = await projectcoordinator.GetByIdAsync(projectCoordinatorId);

            var _project = await project.GetByIdAsync(_projectCortiotor.ProjectId);

            _project.State = "سحب الترخيص"; // أو يمكنك تسميتها "رفض التجديد"
            await project.UpdateAsync(_project);
            toastNotification.AddErrorToastMessage("تم سحب الترخيص وعدم التجديد.");
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Create(int ProjectCoordinatorId)
        {
            
            //var model = new CoordinatorReportVM();/* int ProjectCoordinatorId;*/

            //model.ProjectCoordinatorId = ProjectCoordinatorId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoordinatorReportVM obj)
        {
            try
            {

                var cr = await projectcoordinator.GetByIdAsync(obj.ProjectId, obj.EmployeeId);
                obj.EmployeeId= cr.EmployeeId;
                obj.ProjectId = cr.ProjectId;
                obj.ProjectCoordinatorId = cr.Id;

                var ImageName = FileUpLoader.UploaderFile(obj.Image, "Doc");
                obj.ImageName = ImageName;
                if (ModelState.IsValid == true)
                {
                    obj.Status = "صادر";

                    obj.CreationData = DateTime.Now;
                    var data = mapper.Map<CoordinatorReport>(obj);

                    await coordinatorReport.CreateAsync(data);
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
           
            var data = await coordinatorReport.GetByIdAsync(Id);
            var result = mapper.Map<CoordinatorReportVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CoordinatorReportVM obj)
        {
            try
            {
                var ImageName = FileUpLoader.UploaderFile(obj.Image, "Doc");
                obj.ImageName = ImageName;
                if (ModelState.IsValid == true)
                {
                    obj.Status = "محدث";
                    var data = mapper.Map<CoordinatorReport>(obj);
                    await coordinatorReport.UpdateAsync(data);
                    toastNotification.AddSuccessToastMessage("تم  تعديل بيانات بنجاح.");
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "validation Error";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(obj);
            }
        }
        

      
        public async Task<IActionResult> Details(int Id)
        {
         
            var data = await coordinatorReport.GetByIdAsync(Id);                    
            var result = mapper.Map<CoordinatorReportVM>(data);

            return View(result);
        }

    }
}

#endregion




