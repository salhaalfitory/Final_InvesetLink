using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class ProjectCoordinatorController : Controller
    {
        #region Fields


        private readonly IProjectCoordinator projectCoordinator;
        private readonly IMapper mapper;
       
        #endregion

        //-----------------------------------------
        #region Ctor
        public ProjectCoordinatorController(IProjectCoordinator projectCoordinator, IMapper mapper)
        {
            this.projectCoordinator = projectCoordinator;
            this.mapper = mapper;
           


        }

        #endregion
        //--------------------------------------------------

        #region Actions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await projectCoordinator.GetAllAsync();

            var result = mapper.Map<IEnumerable<ProjectCoordinatorVM>>(data);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectCoordinatorVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<ProjectCoordinator>(obj);

                    await projectCoordinator.CreateAsync(data);
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
            var data = await projectCoordinator.GetByIdAsync(Id);
            var result = mapper.Map<ProjectCoordinatorVM>(data);

            return View(result);
        }

    }
}

#endregion




