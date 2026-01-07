using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = " invalid Email ")]
        public string Email { get; set; }
        //------------------------------------
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "min Len 5")]
        public string Password { get; set; }
        // -----------------------------------------
        public bool RememberMe { get; set; }
    }
}
