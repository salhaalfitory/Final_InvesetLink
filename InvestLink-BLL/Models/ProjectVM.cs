
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Models
{
    public class ProjectVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name  is Required")]
        public string Name { get; set; }//اسم 
      
        public string? State { get; set; } // الحقل المسؤول عن تصنيف الطلب

      
        public string? LegalBodyName { get; set; }//شكل قانوني تحميل

        [Required(ErrorMessage = "LegalBodyFile  is Required")]
        public IFormFile LegalBodyFile { get; set; }//شكل قانوني

        [Required(ErrorMessage = "Area  is Required")]
        public string Area { get; set; }//مجال
        [Required(ErrorMessage = "TypeOfActivity  is Required")]
        public string TypeOfActivity { get; set; }//نوع نشاط
        [Required(ErrorMessage = "ProposedSite   is Required")]
        public string ProposedSite { get; set; }//  موقع مقترح تحميل
        [Required(ErrorMessage = "AreaWanted   is Required")]


        public string AreaWanted { get; set; }//مساحة مطلوبة
        [Required(ErrorMessage = "TypeOfBenefitFromSite  is Required")]
        public string TypeOfBenefitFromSite { get; set; }//نوع انتفاع بموقع
        [Required(ErrorMessage = "projectTimeLine is Required")]
        public string projectTimeLine { get; set; }//مدة تنفيذ مشروع
        [Required(ErrorMessage = "ConstructionPeriod  is Required")]
        public string ConstructionPeriod { get; set; }// العمر الافتراضي
        [Required(ErrorMessage = "LocalLoans  is Required")]

        public string LocalLoans { get; set; }//قروض محلية
        [Required(ErrorMessage = "ForeignLoans  is Required")]
        public string ForeignLoans { get; set; }//قروض اجنبية

        [Required(ErrorMessage = "CostLandBuild   is Required")]
        public string CostLandBuild { get; set; }//الأراضي والمباني والإنشاءات
        [Required(ErrorMessage = "CostMachine  is Required")]
        public string CostMachine { get; set; }//الآلات والمعدات ووسائل النقل
        [Required(ErrorMessage = "CostSetup  is Required")]
        public string CostSetup { get; set; }//مصروفات التأسيس والأثاث
        [Required(ErrorMessage = "TotalCost  is Required")]
        public string TotalCost { get; set; }//إجمالي التكاليف الاستثمارية
        [Required(ErrorMessage = "Technology  is Required")]
        public string Technology { get; set; }//التقنية المستخدمة
        [Required(ErrorMessage = "RawMaterialLocal  is Required")]
        public string RawMaterialLocal { get; set; }//نسبة المواد الخام المحلية
        [Required(ErrorMessage = "RawMaterialForeign   is Required")]

        public string RawMaterialForeign { get; set; }//نسبة المواد الخام الأجنبية




        [Required(ErrorMessage = "AboutTheProject  is Required")]
        public string AboutTheProject { get; set; }//نبذه عن مشروع

        [Required(ErrorMessage = "EnvironmentalImpact  is Required")]

        public string? EnvironmentalImpact { get; set; }//اثر البيئي

        [Required(ErrorMessage = "LocalManpower  is Required")]
        public string LocalManpower { get; set; }//العمالة المحلية 
        [Required(ErrorMessage = "ForeignManpower  is Required")]
        public string ForeignManpower { get; set; }//العمالة الاجنبية
        [Required(ErrorMessage = "TrainingPrograms is Required")]
        public string TrainingPrograms { get; set; }//البرامج التدريبة للعمالة
        [Required(ErrorMessage = "ExperienceOfInvestor  is Required")]
        public string ExperienceOfInvestor { get; set; }//خبرة المستثمر


      
        public DateTime CreationData { get; set; } = DateTime.Now;


        //--------------------------------------



        public virtual List<ProjectInvestor> ProjectInvestors { get; set; } = new List<ProjectInvestor>();

        public virtual List<License>? Licenses { get; set; }
        public virtual List<CoordinatorReportVM>? CoordinatorReports { get; set; }
    }
}