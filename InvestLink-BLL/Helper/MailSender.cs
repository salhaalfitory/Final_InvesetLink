using InvestLink_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Helper
{
    public class MailSender
    {
        public static void SendMail(MailVM model)
        {

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("rehamalwezri@gmail.com", "rgtiilsxivgvklst")
            };

            client.Send("rehamalwezri@gmail.com", model.Email, model.Title, model.Message);
        }
    }
}