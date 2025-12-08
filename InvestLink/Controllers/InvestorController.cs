using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class InvestorController : Controller
    {
        #region Fields


       
        private readonly IMapper mapper;
        private readonly IInvestor investor;



        #endregion

        //-----------------------------------------
        #region Ctor
        public InvestorController( IMapper mapper, IInvestor investor)
        {
           
            this.mapper = mapper;
            this.investor = investor;


        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await investor.GetAllAsync();

            var result = mapper.Map<IEnumerable<InvestorVM>>(data);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InvestorVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Investor>(obj);

                    await investor.CreateAsync(data);
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
            var data = await investor.GetByIdAsync(Id);
            var result = mapper.Map<InvestorVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(InvestorVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Investor>(obj);
                    await investor.UpdateAsync(data);
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
            var data = await investor.GetByIdAsync(Id);
            var result = mapper.Map<InvestorVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(InvestorVM obj)
        {
            try
            {
                var data = mapper.Map<Investor>(obj);
                await investor.DeleteAsync(data);
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
            var data = await investor.GetByIdAsync(Id);
            var result = mapper.Map<InvestorVM>(data);

            return View(result);
        }

    }
}

#endregion



