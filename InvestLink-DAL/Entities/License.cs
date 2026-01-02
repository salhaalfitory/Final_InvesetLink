using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.Entities
{
    [Table("License")]
    public class License
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }//تاريخ إنشاء 
        public DateTime ExpireDate { get; set; }//تاريخ إنتهاء 
        public string LicenseNumber { get; set; }//رقم ترخيص 
        [Required, StringLength(100)]
        public string State { get; set; }//حالة رخصة 
        public string Type { get; set; }//نوع رخصة 
       
        //-------------------------------------------------
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        //----------------------------------
    }
}
