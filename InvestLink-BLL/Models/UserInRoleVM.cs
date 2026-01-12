using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class UserInRoleVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
