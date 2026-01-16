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
            // إعدادات البريد (يجب وضع إيميل وباسورد حقيقيين ليعمل)
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("itstd.5476@uob.edu.ly", "reham1970")
            };

            client.Send("itstd.5476@uob.edu.ly", model.Email, model.Title, model.Message);
        }
    }
}
