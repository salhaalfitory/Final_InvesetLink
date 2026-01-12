using AutoMapper;
using InvestLink_BLL.Helper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_BLL.Repository;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

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
      



        #endregion

        //-----------------------------------------
        #region Ctor
        public CoordinatorReportController(ICoordinatorReport coordinatorReport,ILicense license,IProject project, IProjectCoordinator projectcoordinator, IMapper mapper)
        {
            this.coordinatorReport = coordinatorReport;
            this.license = license;
            this.project = project;
            this.projectcoordinator = projectcoordinator;
            this.mapper = mapper;
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
            obj.ExpireDate = DateTime.Now.AddMonths(3);
            obj.Type = "مزاولة";
            obj.CreatedDate = DateTime.Now;
            obj.LicenseNumber = $"{DateTime.Now.Year}-LIC";

            await license.CreateAsync(obj);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Create(int projectCoordinatorId)
        {
           
           

            var model = new CoordinatorReportVM();

           
            model.ProjectCoordinatorId = projectCoordinatorId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CoordinatorReportVM obj)
        {
            try
            {
                var ImageName = FileUpLoader.UploaderFile(obj.Image, "Doc");
                obj.ImageName = ImageName;
                if (ModelState.IsValid == true)
                {
                    obj.Status = "صادر";
                    obj.ProjectCoordinatorId = 4;
           
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
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var data = await coordinatorReport.GetByIdAsync(Id);
            var result = mapper.Map<CoordinatorReport>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CoordinatorReportVM obj)
        {
            try
            {
                var data = mapper.Map<CoordinatorReport>(obj);
                await coordinatorReport.DeleteAsync(data);
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
         
            var data = await coordinatorReport.GetByIdAsync(Id);                    
            var result = mapper.Map<CoordinatorReportVM>(data);

            return View(result);
        }

    }
}

#endregion




