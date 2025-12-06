using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class InvestorVM
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }//اسم مستثمر
        [EmailAddress(ErrorMessage = " invalid Email ")]
        public string Email { get; set; } //بريد الالكتروني
        [Required(ErrorMessage = "Phone1 is Required")]
        [MinLength(10, ErrorMessage = "min Len 10")]
        [MaxLength(10, ErrorMessage = "max Len 10")]
        public string Phone1 { get; set; }//رقم تلفون 1
        public string? Phone2 { get; set; }//رقم تلفون2
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreationData { get; set; }//تاريخ  تسجيل 
        //------------------------------------
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
        //------------------------------------

        public virtual List<ProjectInvestor>? ProjectInvestors { get; set; }
    }
}
