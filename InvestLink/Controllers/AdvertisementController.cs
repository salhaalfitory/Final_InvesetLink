using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class AdvertisementController : Controller
    {
        #region Fields



        private readonly IAdvertisement advertisement;
        private readonly IMapper mapper;
     


        #endregion

        //-----------------------------------------
        #region Ctor
        public AdvertisementController(IAdvertisement advertisement, IMapper mapper)
        {

            this.advertisement = advertisement;
            this.mapper = mapper;
          

        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await advertisement.GetAllAsync();

            var result = mapper.Map<IEnumerable<AdvertisementVM>>(data);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Advertisement>(obj);

                    await advertisement.CreateAsync(data);
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
            var data = await advertisement.GetByIdAsync(Id);
            var result = mapper.Map<AdvertisementVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdvertisementVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Advertisement>(obj);
                    await advertisement.UpdateAsync(data);
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
            var data = await advertisement.GetByIdAsync(Id);
            var result = mapper.Map<AdvertisementVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(AdvertisementVM obj)
        {
            try
            {
                var data = mapper.Map<Advertisement>(obj);
                await advertisement.DeleteAsync(data);
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
            var data = await advertisement.GetByIdAsync(Id);
            var result = mapper.Map<AdvertisementVM>(data);

            return View(result);
        }

    }
}

#endregion




