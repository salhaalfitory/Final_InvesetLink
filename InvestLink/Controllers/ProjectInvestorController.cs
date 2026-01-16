using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class ProjectInvestorController : Controller
    {
        #region Fields
        private readonly IProjectInvestor projectinvestor;
        private readonly IMapper mapper;
       



        #endregion

        //-----------------------------------------
        #region Ctor
        public ProjectInvestorController(IProjectInvestor projectinvestor, IMapper mapper, ILicense license)
        {
            this.projectinvestor = projectinvestor;
            this.mapper = mapper;
           
         

        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
     
            //var data = await projectinvestor.GetAllAsync();

            //var result = mapper.Map<IEnumerable<ProjectInvestorVM>>(data);
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectInvestorVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<ProjectInvestor>(obj);

                    await projectinvestor.CreateAsync(data);
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
            var data = await projectinvestor.GetByIdAsync(Id);
            var result = mapper.Map<ProjectInvestorVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProjectInvestorVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<ProjectInvestor>(obj);
                    await projectinvestor.UpdateAsync(data);
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
            var data = await projectinvestor.GetByIdAsync(Id);
            var result = mapper.Map<ProjectInvestorVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProjectInvestorVM obj)
        {
            try
            {
                var data = mapper.Map<ProjectInvestor>(obj);
                await projectinvestor.DeleteAsync(data);
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
            var data = await projectinvestor.GetByIdAsync(Id);
            var result = mapper.Map<ProjectInvestorVM>(data);

            return View(result);
        }

    }
}

#endregion



