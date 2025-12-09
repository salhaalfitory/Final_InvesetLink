using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class CoordinatorReportController : Controller
    {
        #region Fields


        private readonly ICoordinatorReport coordinatorReport;
        private readonly IMapper mapper;
      



        #endregion

        //-----------------------------------------
        #region Ctor
        public CoordinatorReportController(ICoordinatorReport coordinatorReport, IMapper mapper)
        {
            this.coordinatorReport = coordinatorReport;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CoordinatorReportVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
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
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<CoordinatorReport>(obj);
                    await coordinatorReport.UpdateAsync(data);
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
        public IActionResult Save()
        {
            return View();
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




