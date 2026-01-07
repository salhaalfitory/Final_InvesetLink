using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class LicenseVM
    {
       

        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }//تاريخ إنشاء 
        public DateTime ExpireDate { get; set; }//تاريخ إنتهاء 
        public string LicenseNumber { get; set; }//رقم ترخيص 
        public string Type { get; set; }//نوع رخصة 
  
        //-------------------------------------------------
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        //----------------------------------
    }
}
