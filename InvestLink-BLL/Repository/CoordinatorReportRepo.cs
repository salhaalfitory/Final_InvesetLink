using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Models;
using InvestLink_DAL.DataBase;
using InvestLink_DAL.Entities;
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
        .ToListAsync();
        }

       
        public async Task<CoordinatorReport> GetByIdAsync(int Id)
        {
            //    AsNoTracking مهمة جداً هنا لتفادي مشاكل التحديث لاحقاً
            var result = await db.CoordinatorReports
                                 .AsNoTracking()
                                 .Include(x => x.ProjectCoordinator)
                                 .ThenInclude(y => y.Project)
                                 .Include(x => x.ProjectCoordinator)
                                 .ThenInclude(y => y.Employee)
                                 //.FirstOrDefaultAsync(x => x.Id ==Id);قديمه
                                 .FirstOrDefaultAsync(x => x.ProjectCoordinatorId == Id);//جديده

            return result;


        }
        public async Task<IEnumerable<CoordinatorReport>> GetAllAsync(int iEmptId)
        {

            return await db.CoordinatorReports
            .Include(x => x.ProjectCoordinator)
            .ThenInclude(y => y.Project)
            .Where(a => a.ProjectCoordinator.Employee.Id == iEmptId)
            .ToListAsync();
        }


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
        public async Task UpdateAsync(CoordinatorReport obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
