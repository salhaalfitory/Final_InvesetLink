using InvestLink_BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace InvestLink.Controllers
{
    public class MailController : Controller
    {
        public IActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(MailVM Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //the sending and receiving in gmil are made possible by the services smtp esrver
                    var smtp = new SmtpClient();//hostname portno
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("itstd.5476@uob.edu.ly", "reham1970");//sender/12345@Aa/yassmen.1985.it@gmail.com
                    smtp.Send("Reham@gmail.com", "Reham@gmail.com", Model.Title, Model.Message);//"yassmen.1985.it@gmail.com","yassmenmamash@gmail.com"
                    TempData["Msg"] = "Succeed";
                    return RedirectToAction("SendEmail");
                }
                return View();

            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Faild" + ex.Message;
                return View();
            }
          
        }
    }
}
