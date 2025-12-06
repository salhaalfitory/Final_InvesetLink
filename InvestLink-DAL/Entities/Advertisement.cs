using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.Entities
{
    [Table("Advertisement")]
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }//اسم 
        [Required, StringLength(500)]
        public string Description { get; set; }//وصف او محتوى
        public string? ImageName { get; set; }//صور
        //-------------------------
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
