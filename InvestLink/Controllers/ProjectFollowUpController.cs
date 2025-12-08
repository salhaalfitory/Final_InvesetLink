using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class ProjectFollowUpController : Controller
    {
        #region Fields


        private readonly IProjectFollowUp projectfollowup;
        private readonly IMapper mapper;
       



        #endregion

        //-----------------------------------------
        #region Ctor
        public ProjectFollowUpController(IProjectFollowUp projectfollowup, IMapper mapper)
        {
            this.projectfollowup = projectfollowup;
            this.mapper = mapper;
           


        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await projectfollowup.GetAllAsync();

            var result = mapper.Map<IEnumerable<ProjectFollowUpVM>>(data);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectFollowUpVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<ProjectFollowUp>(obj);

                    await projectfollowup.CreateAsync(data);
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
            var data = await projectfollowup.GetByIdAsync(Id);
            var result = mapper.Map<ProjectFollowUpVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProjectFollowUpVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<ProjectFollowUp>(obj);
                    await projectfollowup.UpdateAsync(data);
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
            var data = await projectfollowup.GetByIdAsync(Id);
            var result = mapper.Map<ProjectFollowUpVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProjectFollowUpVM obj)
        {
            try
            {
                var data = mapper.Map<ProjectFollowUp>(obj);
                await projectfollowup.DeleteAsync(data);
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
            var data = await projectfollowup.GetByIdAsync(Id);
            var result = mapper.Map<ProjectFollowUpVM>(data);

            return View(result);
        }

    }
}

#endregion




