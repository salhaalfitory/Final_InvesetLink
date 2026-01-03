
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
        [Required]
        public string Name { get; set; }//اسم 
      
        public string? State { get; set; } // الحقل المسؤول عن تصنيف الطلب

       
        public string? LegalBodyName { get; set; }//شكل قانوني تحميل
        public IFormFile LegalBodyFile { get; set; }//شكل قانوني

        [Required]
        public string Area { get; set; }//مجال
        [Required]
        public string TypeOfActivity { get; set; }//نوع نشاط
        [Required]
        public string ProposedSite { get; set; }//  موقع مقترح تحميل
        [Required]
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string AreaWanted { get; set; }//مساحة مطلوبة
        [Required]
        public string TypeOfBenefitFromSite { get; set; }//نوع انتفاع بموقع
        [Required]
        public string projectTimeLine { get; set; }//مدة تنفيذ مشروع
        [Required]
        public string ConstructionPeriod { get; set; }// العمر الافتراضي
        [Required]
  
        public string LocalLoans { get; set; }//قروض محلية
        [Required]
        public string ForeignLoans { get; set; }//قروض اجنبية

        [Required]
        public string CostLandBuild { get; set; }//الأراضي والمباني والإنشاءات
        [Required]
        public string CostMachine { get; set; }//الآلات والمعدات ووسائل النقل
        [Required]
        public string CostSetup { get; set; }//مصروفات التأسيس والأثاث
        [Required]
        public string TotalCost { get; set; }//إجمالي التكاليف الاستثمارية
        [Required]
        public string Technology { get; set; }//التقنية المستخدمة
        [Required]
        public string RawMaterialLocal { get; set; }//نسبة المواد الخام المحلية
        [Required]

        public string RawMaterialForeign { get; set; }//نسبة المواد الخام الأجنبية




        [Required]
        public string AboutTheProject { get; set; }//نبذه عن مشروع

        [Required]

        public string? EnvironmentalImpact { get; set; }//اثر البيئي

        [Required]
        public string LocalManpower { get; set; }//العمالة المحلية 
        [Required]
        public string ForeignManpower { get; set; }//العمالة الاجنبية
        [Required]
        public string TrainingPrograms { get; set; }//البرامج التدريبة للعمالة
        [Required]
        public string ExperienceOfInvestor { get; set; }//خبرة المستثمر


        public string? LicenseName { get; set; }//مسار 
        public IFormFile? LicenseFile { get; set; }//ملف رخصه 
        public DateTime CreationData { get; set; } = DateTime.Now;


        //--------------------------------------



        public virtual List<ProjectInvestor> ProjectInvestors { get; set; } = new List<ProjectInvestor>();

        public virtual List<License>? Licenses { get; set; }
        public virtual List<CoordinatorReportVM>? CoordinatorReports { get; set; }
    }
}