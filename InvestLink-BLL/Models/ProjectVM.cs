//using InvestLink_DAL.Entities;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace InvestLink_BLL.Models
//{
//    public class ProjectVM
//    {
//        [Key]
//        public int Id { get; set; }
//        [Required, StringLength(100)]
//        public string Name { get; set; }//اسم 
//        [Required, StringLength(50)]
//        public string State { get; set; } // الحقل المسؤول عن تصنيف الطلب

//        [Required]
//        public string? LegalBodyName { get; set; }//شكل قانوني تحميل
//        [Required]
//        public string Area { get; set; }//مجال
//        [Required, StringLength(100)]
//        public string TypeOfActivity { get; set; }//نوع نشاط
//        [Required]
//        public string ProposedSite { get; set; }//  موقع مقترح تحميل
//        [Required, StringLength(100)]
//        public string AreaWanted { get; set; }//مساحة مطلوبة
//        [Required, StringLength(100)]
//        public string TypeOfBenefitFromSite { get; set; }//نوع انتفاع بموقع
//        [Required, StringLength(100)]
//        public string projectTimeLine { get; set; }//مدة تنفيذ مشروع
//        [Required, StringLength(100)]
//        public string ConstructionPeriod { get; set; }// العمر الافتراضي
//        [Required, StringLength(100)]
//        public string SourcOfFunding { get; set; }//مصادر تمويل

//        public string ProjectsCapitalCosts { get; set; }//تكاليف رأس مال المشروع
//        [Required, StringLength(500)]
//        public string AboutTheProject { get; set; }//نبذه عن مشروع


//        public string? SourcesOfRawMaterial { get; set; }//مصادر مواد خام
//        public string? EnvironmentalImpact { get; set; }//اثر البيئي

//        [Required]
//        public string LocalManpower { get; set; }//العمالة المحلية 
//        [Required]
//        public string ForeignManpower { get; set; }//العمالة الاجنبية
//        public string? TrainingPrograms { get; set; }//البرامج التدريبة للعمالة
//        public string? ExperienceOfInvestor { get; set; }//خبرة المستثمر

//        public DateTime CreationData { get; set; } = DateTime.Now;


//        //--------------------------------------



//        public virtual List<ProjectInvestor>? ProjectInvestors { get; set; }

//        public virtual List<License>? Licenses { get; set; }
//        public virtual List<CoordinatorReportVM>? CoordinatorReports { get; set; }
//    }
//}
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [Required, StringLength(100)]
        public string Name { get; set; }//اسم 
        //[Required, StringLength(50)]
        [ValidateNever]
        public string State { get; set; } // الحقل المسؤول عن تصنيف الطلب

        [Required]
        public string LegalBodyName { get; set; }//شكل قانوني تحميل
        public IFormFile LegalBodyFile { get; set; }//شكل قانوني

        [Required]
        public string Area { get; set; }//مجال
        [Required, StringLength(100)]
        public string TypeOfActivity { get; set; }//نوع نشاط
        [Required]
        public string ProposedSite { get; set; }//  موقع مقترح تحميل
        [Required, StringLength(100)]
        public string AreaWanted { get; set; }//مساحة مطلوبة
        [Required, StringLength(100)]
        public string TypeOfBenefitFromSite { get; set; }//نوع انتفاع بموقع
        [Required, StringLength(100)]
        public string projectTimeLine { get; set; }//مدة تنفيذ مشروع
        [Required, StringLength(100)]
        public string ConstructionPeriod { get; set; }// العمر الافتراضي
        [Required, StringLength(100)]
        public string SourcOfFunding { get; set; }//مصادر تمويل
        public string LocalLoans { get; set; }//قروض محلية
        public string ForeignLoans { get; set; }//قروض اجنبية


        public string CostLandBuild { get; set; }//الأراضي والمباني والإنشاءات
        public string CostMachine { get; set; }//الآلات والمعدات ووسائل النقل
        public string CostSetup { get; set; }//مصروفات التأسيس والأثاث

        public string TotalCost { get; set; }//إجمالي التكاليف الاستثمارية

        public string Technology { get; set; }//التقنية المستخدمة

        public string RawMaterialLocal { get; set; }//نسبة المواد الخام المحلية


        public string RawMaterialForeign { get; set; }//نسبة المواد الخام الأجنبية




        [Required, StringLength(500)]
        public string AboutTheProject { get; set; }//نبذه عن مشروع



        public string? EnvironmentalImpact { get; set; }//اثر البيئي

        [Required]
        public string LocalManpower { get; set; }//العمالة المحلية 
        [Required]
        public string ForeignManpower { get; set; }//العمالة الاجنبية
        public string? TrainingPrograms { get; set; }//البرامج التدريبة للعمالة
        public string? ExperienceOfInvestor { get; set; }//خبرة المستثمر

        public DateTime CreationData { get; set; } = DateTime.Now;


        //--------------------------------------



        public virtual List<ProjectInvestor>? ProjectInvestors { get; set; }

        public virtual List<License>? Licenses { get; set; }
        public virtual List<CoordinatorReportVM>? CoordinatorReports { get; set; }
    }
}