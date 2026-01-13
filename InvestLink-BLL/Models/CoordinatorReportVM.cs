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
    public class CoordinatorReportVM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "يرجى إدخال وصف التقرير")]
        public string Description { get; set; }// وصف تقرير
        public DateTime CreationData { get; set; } = DateTime.Now;// تاريخ إنشاء تقرير
        public string? ImageName { get; set; }// صور
      public IFormFile? Image { get; set; }
        public string? Status { get; set; }// حالة تقرير
        public int projectId { get; set; }
        public int employeeId { get; set; }

        //-------------------------------------------------
        public int ProjectCoordinatorId { get; set; }
        public ProjectCoordinator? ProjectCoordinator { get; set; }
        //----------------------------------
    }
}
