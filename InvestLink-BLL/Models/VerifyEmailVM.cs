using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class VerifyEmailVM
{
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "يرجى إدخال رمز التحقق")]
        [Display(Name = "رمز التحقق")]
        public string Code { get; set; }
    }
}
