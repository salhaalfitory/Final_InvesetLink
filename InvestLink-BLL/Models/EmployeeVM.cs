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
        public EmployeeVM()
        {
            this.IsActive = true;
            this.IsDeleted = false;
            this.CreationData = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }//اسم مستثمر
        [EmailAddress(ErrorMessage = " invalid Email ")]
        public string Email { get; set; }//بريد الالكتروني
        
        
        //--------------------------------------
        public string? Password { get; set; }

        public string? Role { get; set; }
        public bool IsActive { get; set; } 
        public bool IsDeleted { get; set; }
        public DateTime CreationData { get; set; } = DateTime.Now;//تاريخ تسجيل
        


        public virtual List<ProjectCoordinator>? ProjectFollowUps { get; set; }
        public virtual List<Advertisement>? Advertisements { get; set; }
    }
}
