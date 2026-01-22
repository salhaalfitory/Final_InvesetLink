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

        [Required(ErrorMessage = "Description")]
        public string Description { get; set; }// وصف تقرير
        public DateTime CreationData { get; set; } = DateTime.Now;// تاريخ إنشاء تقرير
        public string? ImageName { get; set; }// صور
        public bool IsUpdated { get; set; }
        public DateTime UpdatedDate { get; set; }// تاريخ بداية

        public IFormFile? Image { get; set; }
        public string? Status { get; set; }// حالة تقرير
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        //-------------------------------------------------
        public int ProjectCoordinatorId { get; set; }
        public ProjectCoordinator? ProjectCoordinator { get; set; }
        //----------------------------------
    }
}
