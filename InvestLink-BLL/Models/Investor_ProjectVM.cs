using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class Investor_ProjectVM
    {
        public ProjectVM Project { get; set; }
        public List<InvestorVM> Investors { get; set; } = new List<InvestorVM>();
        public int SubmitedInvestorId { get; set; } 

    }
}
