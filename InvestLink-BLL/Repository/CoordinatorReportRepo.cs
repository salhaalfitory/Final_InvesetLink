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
            .Include(x => x.ProjectCoordinator)       
            .ThenInclude(y => y.Project)          
            .ToListAsync();
        }


        //CoordinatorReportId تجيب ف 
        public async Task<CoordinatorReport> GetByIdAsync(int Id)
        {
            var data = await db.CoordinatorReports
                               .Include(x => x.ProjectCoordinator) // لجلب بيانات المنسق صاحب التقرير
                               .FirstOrDefaultAsync(x => x.Id == Id); // البحث برقم التقرير

            return data;
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
