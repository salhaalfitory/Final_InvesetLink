using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.Entities
{
    [Table("Nationality")]
    public class Nationality
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }//اسم جنسية

        //-----------------------------------------------
        public virtual List<Investor>? Investors { get; set; }
    }
}
