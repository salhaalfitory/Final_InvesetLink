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
    public class ProjectCoordinatorRepo : IProjectCoordinator
    {
        private readonly MyContext db;

        public ProjectCoordinatorRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(ProjectCoordinator obj)
        {
            await db.ProjectCoordinators.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProjectCoordinator obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectCoordinator>> GetAllAsync()
        {
            var data = await db.ProjectCoordinators.ToListAsync();
            return data;
        }

        public async Task<ProjectCoordinator> GetByIdAsync(int Id)
        {
            var data = await db.ProjectCoordinators.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public  async Task UpdateAsync(ProjectCoordinator obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
