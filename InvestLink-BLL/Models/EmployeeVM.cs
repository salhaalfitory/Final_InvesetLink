using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class EmployeeVM
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }//اسم مستثمر
        [EmailAddress(ErrorMessage = " invalid Email ")]
        public string Emial { get; set; }//بريد الالكتروني
        [RegularExpression("09[0-9]{8}", ErrorMessage = "0912345678: يجب ان يتكون الرقم من عشر ارقام ويبدأ 09 مثل ")]
        [MinLength(10)]
        [MaxLength(10)]
        public string FirstPhoneNumber { get; set; }//رقم تلفون 1
        [RegularExpression("09[0-9]{8}", ErrorMessage = "0912345678: يجب ان يتكون الرقم من عشر ارقام ويبدأ 09 مثل ")]
        [MinLength(10)]
        [MaxLength(10)]
        public string? SecondPhoneNumber { get; set; }//رقم تلفون2
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationData { get; set; } = DateTime.Now;//تاريخ تسجيل
        //------------------------------
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
        //------------------------------------


        public virtual List<ProjectFollowUp>? ProjectFollowUps { get; set; }
        public virtual List<Advertisement>? Advertisements { get; set; }
    }
}
