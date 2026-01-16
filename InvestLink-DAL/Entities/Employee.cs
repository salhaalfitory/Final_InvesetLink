using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }//اسم مستثمر
        [EmailAddress(ErrorMessage = " invalid Email ")]
        public string Email { get; set; }//بريد الالكتروني
        
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationData { get; set; } = DateTime.Now;//تاريخ تسجيل
        

        public virtual List<ProjectCoordinator>? ProjectCoordinators { get; set; }
        public virtual List<Advertisement>? Advertisements { get; set; }
    }
}
