using AutoMapper;
using Humanizer;
using InvestLink_BLL.Helper;
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
        private readonly IProjectInvestor projectinvestor;

        
        #endregion

        //-----------------------------------------
        #region Ctor
        public ProjectController(IProject project, IMapper mapper, ILicense license, IInvestor investor, IProjectInvestor projectinvestor)
        {
            this.project = project;
            this.mapper = mapper;
            this.license = license;
            this.investor = investor;
            this.projectinvestor = projectinvestor;
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
        public async Task<IActionResult> Create(Investor_ProjectVM obj)
        {
            try
            {
                var LegalBodyName = FileUpLoader.UploaderFile(obj.LegalBodyFile, "Doc");
                var ImageName = FileUpLoader.UploaderFile(obj.Image, "Doc");


                if (ModelState.IsValid == true)
                {

                    
                    var Project_info = mapper.Map<Project>(obj.Project);
                    Project_info.LegalBodyName = LegalBodyName;

                    await project.CreateAsync(Project_info);


                    if (obj.Investors != null && obj.Investors.Any())
                    {

                        var Investors_info = mapper.Map<List<Investor>>(obj.Investors);
                      
                        foreach (var item in Investors_info)
                        {
                            item.ImageName = ImageName;
                            await investor.CreateAsync(item);

                            var Link = new ProjectInvestor
                            {
                                ProjectId = Project_info.Id,
                                InvestorId = item.Id
                            };

                            await projectinvestor.CreateAsync(Link);
                        }
  

                        return RedirectToAction("Index");

                    }

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



