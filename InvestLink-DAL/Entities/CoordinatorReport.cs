using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.Entities
{

    [Table("CoordinatorReport")]
    public class CoordinatorReport
    {
        [Key]
        public int Id { get; set; }

     
        public string Description { get; set; }// وصف تقرير
        public DateTime CreationData { get; set; } // تاريخ إنشاء تقرير
        public string? ImageName { get; set; }// صور
        [Required, StringLength(100)]
        public string? Status { get; set; }// حالة تقرير 
        public bool IsUpdated { get; set; }
        public DateTime UpdateData { get; set; } 





        //-------------------------------------------------
        public int ProjectCoordinatorId { get; set; }
        public ProjectCoordinator? ProjectCoordinator { get; set; }
        //----------------------------------
    }
}
