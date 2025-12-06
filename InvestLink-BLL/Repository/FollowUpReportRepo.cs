using InvestLink_BLL.Interfaces;
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
    public class FollowUpReportRepo : IFollowUpReport
    {
        private readonly MyContext db;

        public FollowUpReportRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(FollowUpReport obj)
        {
            await db.FollowUpReports.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(FollowUpReport obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FollowUpReport>> GetAllAsync()
        {
            var data = await db.FollowUpReports.ToListAsync();
            return data;
        }

        public async Task<FollowUpReport> GetByIdAsync(int Id)
        {
            var data = await db.FollowUpReports.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public async Task UpdateAsync(FollowUpReport obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
