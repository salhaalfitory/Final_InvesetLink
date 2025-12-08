using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.Entities
{

    [Table("FollowUpReport")]
    public class FollowUpReport
    {
        [Key]
        public int Id { get; set; }
       
        [Required, StringLength(500)]
        public string Description { get; set; }// وصف تقرير
        public DateTime CreationData { get; set; } = DateTime.Now;// تاريخ إنشاء تقرير
        public string? ImageName { get; set; }// صور
        [Required, StringLength(100)]
        public string? Status { get; set; }// حالة تقرير

        //-------------------------------------------------
        public int ProjectFollowUpId { get; set; }
        public ProjectFollowUp? ProjectFollowUp { get; set; }
        //----------------------------------
    }
}
