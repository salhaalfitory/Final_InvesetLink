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
    public class ProjectInvestorRepo : IProjectInvestor
    {
        private readonly MyContext db;

        public ProjectInvestorRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(ProjectInvestor obj)
        {
            await db.ProjectInvestors.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProjectInvestor obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

      

        public async Task<IEnumerable<ProjectInvestor>> GetAllAsync(int invetorId)
        {
            var data = await db.ProjectInvestors.Where(p => p.InvestorId == invetorId).ToListAsync();
            return data;
        }

        public async Task<ProjectInvestor> GetByIdAsync(int Id)
        {
            var data = await db.ProjectInvestors.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public async Task UpdateAsync(ProjectInvestor obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
