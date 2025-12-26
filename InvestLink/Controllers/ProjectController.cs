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

                if (obj.Project.LegalBodyFile != null)
                {
                    var LegalBodyName = FileUpLoader.UploaderFile(obj.Project.LegalBodyFile, "Doc");
                    obj.Project.LegalBodyName = LegalBodyName;
                }


                if (ModelState.IsValid == true)
                {

                    var Project_info = mapper.Map<Project>(obj.Project);

                    var Project_info_Id = await project.CreateAsync(Project_info);


                    if (obj.Investors != null && obj.Investors.Any())
                    {
                        foreach (var item in obj.Investors)
                        {
                            if (item.Image != null)
                            {
                                var ImageName = FileUpLoader.UploaderFile(item.Image, "Doc");
                                item.ImageName = ImageName;
                            }

                            var submittedInvestor = await investor.GetByEmailAsync(item.Email);
                            int investorId;

                            if (submittedInvestor != null)
                            {
                                // Existing investor
                                investorId = submittedInvestor.Id;
                            }
                            else
                            {
                                // New investor
                                var newInvestor = mapper.Map<Investor>(item);
                                investorId = await investor.CreateAsync(newInvestor);
                            }
                            var link = new ProjectInvestor
                            {
                                ProjectId = Project_info_Id,
                                InvestorId = investorId
                            };

                            await projectinvestor.CreateAsync(link);
                        }

                        return RedirectToAction("Index");
                    }
  
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



