using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.DataBase;
using InvestLink_DAL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Repository
{
    public class CoordinatorReportRepo : ICoordinatorReport
    {
        private readonly MyContext db;

        public CoordinatorReportRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(CoordinatorReport obj)
        {            
            await db.CoordinatorReports.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(CoordinatorReport obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CoordinatorReport>> GetAllAsync()
        {

            return await db.CoordinatorReports
            .Include(x => x.ProjectCoordinator)       // 1. ضروري: إحضار بيانات المنسق المرتبط بالتقرير
            .ThenInclude(y => y.Project)          // 2. ضروري جداً: الدخول لجدول المنسق وإحضار اسم المشروع
            //.Where(a => a.ProjectCoordinator.Employee.Id == iEmptId) // تصفية التقارير بناءً على معرف المشروع
            .ToListAsync();
        }
        public async Task<IEnumerable<CoordinatorReport>> GetAllAsync(int iEmptId)
        {

            return await db.CoordinatorReports
            .Include(x => x.ProjectCoordinator)     
            .ThenInclude(y => y.Project)         
            .Where(a => a.ProjectCoordinator.Employee.Id == iEmptId) 
            .ToListAsync();
        }


        //public async Task<IEnumerable<Project>> GetAllAsync(IEnumerable<ProjectInvestor> projectInvestors)
        public async Task<IEnumerable<CoordinatorReport>> GetAllAsync(IEnumerable<CoordinatorReport> iicoordinatorReport)
        {
            var data = new List<CoordinatorReport>();

            if (iicoordinatorReport != null)
            {
                foreach (var cr in iicoordinatorReport)
                {
                    data.Add(await db.CoordinatorReports.FindAsync(cr.ProjectCoordinatorId));
                }
            }
            return data;
        }


            //return await db.CoordinatorReports
            //.Include(x => x.ProjectCoordinator)       // 1. ضروري: إحضار بيانات المنسق المرتبط بالتقرير
            //.ThenInclude(y => y.Project)          // 2. ضروري جداً: الدخول لجدول المنسق وإحضار اسم المشروع
            ////.Where(cr => cr.ProjectCoordinator.Project.Id ==) // تصفية التقارير بناءً على معرف المشروع
            //.ToListAsync();
      

        //public async Task<IEnumerable<CoordinatorReport>> GetAllAsync(IEnumerable<CoordinatorReport> coordinatorReports) { 
                                      
        //    List<CoordinatorReport> data = new List<CoordinatorReport>();
        //    if (coordinatorReports != null)
        //    {
        //        foreach (CoordinatorReport coordinatorReport in coordinatorReports)
        //        {

        //            data.Add(await db.CoordinatorReports.FindAsync(coordinatorReports.ProjectCoordinatorId));
        //        }
        //    }
        //    return data;
        //}
        //public async Task<CoordinatorReport> GetByIdAsync(int Id)
        //{


        //    return await db.CoordinatorReports
        //                .Include(x => x.ProjectCoordinator)      // جلب بيانات المنسق
        //                    .ThenInclude(y => y.Project)         // جلب بيانات المشروع التابع للمنسق
        //                .Include(x => x.ProjectCoordinator)
        //                    .ThenInclude(y => y.Employee)        // جلب بيانات الموظف (اختياري)
        //                .FirstOrDefaultAsync(x => x.Id == Id);
        //}
        public async Task<CoordinatorReport?> GetByIdAsync(int Id)
        {
            // AsNoTracking مهمة جداً هنا لتفادي مشاكل التحديث لاحقاً
            var result = await db.CoordinatorReports
                                 .AsNoTracking()
                                 .Include(x => x.ProjectCoordinator)
                                 .ThenInclude(y => y.Project)
                                 .Include(x => x.ProjectCoordinator)
                                 .ThenInclude(y => y.Employee)
                                 .FirstOrDefaultAsync(x => x.Id == Id);

            return result;
        }

        public async Task UpdateAsync(CoordinatorReport obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
