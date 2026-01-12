using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class RoleVM
    {
        [Required(ErrorMessage = "Name Role is Required")]
        public string Name { get; set; }
    }
}
