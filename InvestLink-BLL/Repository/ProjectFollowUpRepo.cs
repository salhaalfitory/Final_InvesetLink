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
    public class ProjectFollowUpRepo : IProjectFollowUp
    {
        private readonly MyContext db;

        public ProjectFollowUpRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(ProjectFollowUp obj)
        {
            await db.ProjectFollowUps.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProjectFollowUp obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectFollowUp>> GetAllAsync()
        {
            var data = await db.ProjectFollowUps.ToListAsync();
            return data;
        }

        public async Task<ProjectFollowUp> GetByIdAsync(int Id)
        {
            var data = await db.ProjectFollowUps.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public  async Task UpdateAsync(ProjectFollowUp obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
