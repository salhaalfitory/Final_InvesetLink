using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvestLink.Controllers
{
    public class NationalityController : Controller
    {
        #region Fields


        private readonly INationality nationality;
        private readonly IMapper mapper;
      



        #endregion

        //-----------------------------------------
        #region Ctor
        public NationalityController(INationality nationality, IMapper mapper)
        {
            this.nationality = nationality;
            this.mapper = mapper;
           


        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await nationality.GetAllAsync();

            var result = mapper.Map<IEnumerable<NationalityVM>>(data);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NationalityVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Nationality>(obj);

                    await nationality.CreateAsync(data);
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
            var data = await nationality.GetByIdAsync(Id);
            var result = mapper.Map<NationalityVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(NationalityVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Nationality>(obj);
                    await nationality.UpdateAsync(data);
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
            var data = await nationality.GetByIdAsync(Id);
            var result = mapper.Map<NationalityVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(NationalityVM obj)
        {
            try
            {
                var data = mapper.Map<Nationality>(obj);
                await nationality.DeleteAsync(data);
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
            var data = await nationality.GetByIdAsync(Id);
            var result = mapper.Map<NationalityVM>(data);

            return View(result);
        }

    }
}

#endregion



