using AutoMapper;
using InvestLink_BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InvestLink.Controllers
{
    public class HomeController : Controller
    {
        #region Fields



 
        private readonly IMapper mapper;
     


        #endregion

        //-----------------------------------------
        #region Ctor
        public HomeController(IMapper mapper)
        {
            
            this.mapper = mapper;
           


        }

        #endregion
        //--------------------------------------------------

        #region Actions
        public async Task<IActionResult> Index()
        {
            return View();

        }
       
    }
}

#endregion



