using AutoMapper;
using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvestLink.Controllers
{
    public class EmployeeController : Controller
    {

        #region Fields


        private readonly IEmployee employee;
        private readonly INationality nationality;
        private readonly IMapper mapper;



        #endregion

        //-----------------------------------------
        #region Ctor
        public EmployeeController(IEmployee employee, INationality nationality, IMapper mapper)
        {
            this.employee = employee;
            this.nationality = nationality;
            this.mapper = mapper;


        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await employee.GetAllAsync();

            var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
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
        public async Task<IActionResult> Create(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Employee>(obj);

                    await employee.CreateAsync(data);
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
            var data = await employee.GetByIdAsync(Id);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var data = mapper.Map<Employee>(obj);
                    await employee.UpdateAsync(data);
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
            var data = await employee.GetByIdAsync(Id);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM obj)
        {
            try
            {
                var data = mapper.Map<Employee>(obj);
                await employee.DeleteAsync(data);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;

                return View(obj);
            }
        }
       
        public async Task<IActionResult> Details(int Id)
        {
            var data = await employee.GetByIdAsync(Id);
            var result = mapper.Map<EmployeeVM>(data);

            return View(result);
        }

    }
}

#endregion



