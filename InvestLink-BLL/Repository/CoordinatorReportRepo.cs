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
            var data = await db.CoordinatorReports.ToListAsync();
            return data;
        }

        public async Task<CoordinatorReport> GetByIdAsync(int Id)
        {
            var data = await db.CoordinatorReports.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public async Task UpdateAsync(CoordinatorReport obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
