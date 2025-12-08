using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.Entities
{
    public class ProjectFollowUp
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }// تاريخ بداية
        public DateTime EndDate { get; set; }//تاريخ نهاية
        //-------------------------------------------
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        //-------------------------------------------
        public int ProjectId { get; set; }
    
        public Project? Project { get; set; }
        //--------------------------------------
        public virtual List<FollowUpReport>? FollowUpReports { get; set; }
    }
}
