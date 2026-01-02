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
using static System.Runtime.InteropServices.JavaScript.JSType;
using Project = InvestLink_DAL.Entities.Project;

namespace InvestLink.Controllers
{
    public class ProjectController : Controller
    {
        #region Fields


        private readonly IProject project;
        private readonly IMapper mapper;
        private readonly ILicense license;
        private readonly INationality nationality;
        private readonly IInvestor investor;
        private readonly IProjectInvestor projectinvestor;

        
        #endregion

        //-----------------------------------------
        #region Ctor
        public ProjectController(IProject project, IMapper mapper, ILicense license, INationality nationality, IInvestor investor, IProjectInvestor projectinvestor)
        {
            this.project = project;
            this.mapper = mapper;
            this.license = license;
            this.nationality = nationality;
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
        public async Task<IActionResult> Create()
        {
            var nat = await nationality.GetAllAsync();
            ViewBag.NationalityList = new SelectList(nat, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Investor_ProjectVM obj)
        {
            try
            {
                    var LegalBodyName = FileUpLoader.UploaderFile(obj.Project.LegalBodyFile, "Doc");
                    obj.Project.LegalBodyName = LegalBodyName;


                if (ModelState.IsValid == true)
                {

                    obj.Project.State = "تم استلام";

                    var Project_info = mapper.Map<Project>(obj.Project);


                    var Project_info_Id = await project.CreateAsync(Project_info);


                    if (obj.Investors != null && obj.Investors.Any())
                    {
                        foreach (var item in obj.Investors)
                        {
                           
                                var ImageName = FileUpLoader.UploaderFile(item.Image, "Doc");
                                item.ImageName = ImageName;


                                var submittedInvestor = await investor.GetByEmailAsync(item.Email);

                                int investorId;
                                if (submittedInvestor != null)
                                {
                                    //existing investor
                                    investorId = submittedInvestor.Id;

                                }
                                else
                                {
                                    //New investor
                                    var newInvestor = mapper.Map<Investor>(item);
                                    investorId = await investor.CreateAsync(newInvestor);
                                }

                                var Link = new ProjectInvestor
                                {
                                    ProjectId = Project_info_Id,
                                    InvestorId = investorId
                                };

                                await projectinvestor.CreateAsync(Link);
                            }

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
         

       

       
       
        
        public async Task<IActionResult> Details(int Id)
        {
            var data = await project.GetByIdAsync(Id);
            var result = mapper.Map<Investor_ProjectVM>(data);
            var nat = mapper.Map<IEnumerable<NationalityVM>>(await nationality.GetAllAsync());
            ViewBag.NationalityList = new SelectList(nat, "Id", "Name");

            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> NewRequests()
        {

            var data = await project.GetByStateAsync("تم استلام");
            var result = mapper.Map<IEnumerable<ProjectVM>>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> SendToAdmin(int Id)
        {
            var request = await project.GetByIdAsync(Id);

            request.State = "قيد المراجعة من الإدارة";
            await project.UpdateAsync(request);

            TempData["Message"] = "تم إرسال الطلب إلى الإدارة بنجاح ✅";
            return RedirectToAction("RequestsReferredtomanagement");

        }


        [HttpPost]
        public async Task<IActionResult> Reject(int Id)
        {
            var request = await project.GetByIdAsync(Id);


            request.State = "مرفوض";
            await project.UpdateAsync(request);

            TempData["Message"] = "تم رفض الطلب ❌";
            return RedirectToAction("RejectedRequests");
        }
        [HttpGet]
        public async Task<IActionResult> RejectedRequests()
        {
            var data = await project.GetByStateAsync("مرفوض");
            var result = mapper.Map<IEnumerable<ProjectVM>>(data);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> RequestsReferredtomanagement()
        {
            var data = await project.GetByStateAsync("قيد المراجعة من الإدارة");
            var result = mapper.Map<IEnumerable<ProjectVM>>(data);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> ApproveFinal()
        {

            var data = await project.GetByStateAsync("معتمد");

            var result = mapper.Map<IEnumerable<ProjectVM>>(data);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> ApproveFinal(int Id)
        {         
        try {
            var request = await project.GetByIdAsync(Id);

            request.State = "معتمد";
            await project.UpdateAsync(request);
           InvestLink_DAL.Entities.License obj = new InvestLink_DAL.Entities.License();

            obj.ProjectId = request.Id;
            obj.CreatedDate = DateTime.Now;
            obj.ExpireDate = DateTime.Now.AddMinutes(2);
            obj.State = "سارية المفعول";
            obj.Type = "رخصة استثمارية";
                // توليد رقم رخصة تلقائي مميز
                //ssssٍسٍ مثال: 2025-569-LIC
                obj.LicenseNumber = $"{DateTime.Now.Year}-LIC";
                await license.CreateAsync(obj);

           TempData["Message"] = "تم اعتماد الطلب وإصدار الرخصة بنجاح";
           return RedirectToAction("ApproveFinal");

            }

            catch (Exception ex)
            {
               TempData["Error"] = "حدث خطأ أثناء عملية الاعتماد: " + ex.Message;        
               return RedirectToAction("ApproveFinal");
            }
            //TempData["Message"] = "تم اعتماد الطلب نهائياً ✅";
            //return RedirectToAction("ApproveFinal");
        }

      
        [HttpPost]
        public async Task<IActionResult> RejectFinal(int Id)
        {
            var request = await project.GetByIdAsync(Id);

            request.State = "مرفوض ";
            await project.UpdateAsync(request);

            TempData["Message"] = "تم رفض الطلب نهائياً ";
            return RedirectToAction("Details", new { Id });
        }


    }
}

#endregion



