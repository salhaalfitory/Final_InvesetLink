using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class FollowUpReportController : Controller
    {
        #region Fields


        private readonly IFollowUpReport followupreport;
        private readonly IMapper mapper;
      



        #endregion

        //-----------------------------------------
        #region Ctor
        public FollowUpReportController(IFollowUpReport followupreport, IMapper mapper)
        {
            this.followupreport = followupreport;
            this.mapper = mapper;
         


        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await followupreport.GetAllAsync();

            var result = mapper.Map<IEnumerable<FollowUpReportVM>>(data);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FollowUpReportVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<FollowUpReport>(obj);

                    await followupreport.CreateAsync(data);
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
            var data = await followupreport.GetByIdAsync(Id);
            var result = mapper.Map<FollowUpReportVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(FollowUpReportVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<FollowUpReport>(obj);
                    await followupreport.UpdateAsync(data);
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
            var data = await followupreport.GetByIdAsync(Id);
            var result = mapper.Map<FollowUpReportVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(FollowUpReportVM obj)
        {
            try
            {
                var data = mapper.Map<FollowUpReport>(obj);
                await followupreport.DeleteAsync(data);
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
            var data = await followupreport.GetByIdAsync(Id);
            var result = mapper.Map<FollowUpReportVM>(data);

            return View(result);
        }

    }
}

#endregion




