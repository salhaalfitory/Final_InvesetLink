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
    public class EmployeeProjectRepo : IEmployeeProject
    {
        private readonly MyContext db;

        public EmployeeProjectRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(EmployeeProject obj)
        {
            await db.EmployeeProjects.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmployeeProject obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeProject>> GetAllAsync()
        {
            var data = await db.EmployeeProjects.ToListAsync();
            return data;
        }

        public async Task<EmployeeProject> GetByIdAsync(int Id)
        {
            var data = await db.EmployeeProjects.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public  async Task UpdateAsync(EmployeeProject obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
