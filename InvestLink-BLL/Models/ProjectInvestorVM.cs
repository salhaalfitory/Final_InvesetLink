using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class ProjectInvestorVM
    {
        public int Id { get; set; }

        //----------------------------------
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        //----------------------------------

        public int InvestorId { get; set; }
        public Investor? Investor { get; set; }
    }
}
