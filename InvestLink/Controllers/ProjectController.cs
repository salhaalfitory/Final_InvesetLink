using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Project = InvestLink_DAL.Entities.Project;

namespace InvestLink.Controllers
{
    public class ProjectController : Controller
    {
        #region Fields


        private readonly IProject project;
        private readonly IMapper mapper;
        private readonly ILicense license;
        private readonly IInvestor investor;
        

        #endregion

        //-----------------------------------------
        #region Ctor
        public ProjectController(IProject project, IMapper mapper, ILicense license, IInvestor investor)
        {
            this.project = project;
            this.mapper = mapper;
            this.license = license;
            this.investor = investor;
        }
        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await project.GetAllAsync();

            var result = mapper.Map<IEnumerable<ProjectVM>>(data);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //public async Task<IActionResult> Create(ProjectVM obj)
        public async Task<IActionResult> Create(ProjectInvestorVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var Project_info = mapper.Map<Project>(obj.Project);
                    await project.CreateAsync(Project_info);


                   


                    if (obj.Investor != null )
                    {

                        var Investors_info = mapper.Map<List<Investor>>(obj.Investor);

                        foreach (var inv in Investors_info)
                        {

                            inv.Id = Project_info.Id;

                            await investor.CreateAsync(inv);
                        }
                        return RedirectToAction("Index");

                    }
                   

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
            var data = await project.GetByIdAsync(Id);
            var result = mapper.Map<ProjectVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProjectVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Project>(obj);
                    await project.UpdateAsync(data);
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
       
        
        public async Task<IActionResult> Details(int Id)
        {
            var data = await project.GetByIdAsync(Id);
            var result = mapper.Map<ProjectVM>(data);

            return View(result);
        }

    }
}

#endregion



