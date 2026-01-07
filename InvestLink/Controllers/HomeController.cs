using AutoMapper;
using InvestLink_BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InvestLink.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]

        public async Task<IActionResult> Index()
        {
            return View();

        }
       
    }
}




