using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class Invesitor_ProjectVM
    {
        public Project Project { get; set; }
        public List<Investor> Investors { get; set; } = new List<Investor>();
    }
}
