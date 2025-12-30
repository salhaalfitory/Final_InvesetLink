using AutoMapper;
using Humanizer;
using InvestLink_BLL.Helper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly INationality nationality;


        #endregion

        //-----------------------------------------
        #region Ctor
        public ProjectController(IProject project, IMapper mapper, ILicense license, IInvestor investor, IProjectInvestor projectinvestor, INationality nationality)
        {
            this.project = project;
            this.mapper = mapper;
            this.license = license;
            this.investor = investor;
            this.projectinvestor = projectinvestor;
            this.nationality = nationality;
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
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    //ViewBag.Nationalities = new SelectList(_context.Nationalities, "Id", "Name");
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var nat = await nationality.GetAllAsync();
            ViewBag.NationalityList = new SelectList(nat, "Id", "Name");
            return View();
        }

        [HttpPost]
                                //id اللي جاي فالrequest =0) 
                                //منعرفش id  الإ لما يتكريت
        public async Task<IActionResult> Create(Investor_ProjectVM obj)
        {
            try
            {

               var LegalBodyName = FileUpLoader.UploaderFile(obj.Project.LegalBodyFile, "Doc");
               obj.Project.LegalBodyName = LegalBodyName;

                //obj.Project.State = "تم استلام";

                if (ModelState.IsValid == true)
                {
                    obj.Project.State = "تم استلام";

                    //obj.Project.State = "تم استلام";

                    var Project_info = mapper.Map<Project>(obj.Project);

                    var Project_info_Id = await project.CreateAsync(Project_info);


                    if (obj.Investors != null && obj.Investors.Any())
                    {
                        //var Investors_info = mapper.Map<List<Investor>>(obj.Investors);

                        //mapping عشان ميريحش قبل مندير  IFormFile
                        foreach (var item in obj.Investors)
                        {
                            if (item.Image != null)
                            {
                                var ImageName = FileUpLoader.UploaderFile(item.Image, "Doc");
                                item.ImageName = ImageName;
                             }
                            //اللي توا دار سبميت investorIdال 
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

                                //لما نديرmapping   ImageName تكون واتيه 
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

                        //لما نديرmapping   ImageName تكون واتيه 
                        //var Investors_info = mapper.Map<List<Investor>>(obj.Investors);

                        //foreach (var item in Investors_info)
                        //{
                        //  var Investors_info_Id = await investor.CreateAsync(item);

                        //    var Link = new ProjectInvestor
                        //    {
                        //        ProjectId = Project_info_Id,
                        //        InvestorId = Investors_info_Id
                        //    };

                        //await projectinvestor.CreateAsync(Link);
                        //}
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


        //[HttpPost]
        //public async Task<IActionResult> Create(Invesitor_ProjectVM obj)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid == true)
        //        {
        //            var Project_info = mapper.Map<Project>(obj.Project);
        //            await project.CreateAsync(Project_info);
        //            //var Investors_info = mapper.Map<Investor>(obj.Investor);
        //            if (obj.Investors != null && obj.Investors.Any())
        //            {
        //                obj.Investors = obj.Investors;

        //                //var Investors_info = mapper.Map<List<Investor>>(obj.Investor);

        //                foreach (var inv in obj.Investors)
        //                {
        //                    investor.Id = obj.Project; // ربط المستثمر بهذا المشروع

        //                    //inv.Id = Project_info.Id;
        //                }                
        //                var Investors_info = mapper.Map<List<Investor>>(obj.Investor);

        //                await investor.CreateAsync(Investors_info);

        //                return RedirectToAction("Index");

        //            }
        //            //await investor.CreateAsync(Investors_info);

        //        }
        //        TempData["Meesage"] = "validation Error";
        //        return View(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Message"] = ex.Message;
        //        return View(obj);
        //    }
        //}

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



