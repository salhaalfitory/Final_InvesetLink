using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{

    public class ResetPasswordVM
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Token { get; set; } // ضروري جداً لتمرير التوكن
} }