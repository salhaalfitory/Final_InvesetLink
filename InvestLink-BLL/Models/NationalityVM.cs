using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
   public class NationalityVM
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }//اسم جنسية

        //-----------------------------------------------
        public virtual List<Investor>? Investors { get; set; }
        public virtual List<Employee>? Employees { get; set; }
    }
}
