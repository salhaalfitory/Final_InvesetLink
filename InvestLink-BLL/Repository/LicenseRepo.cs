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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InvestLink_BLL.Repository
{
    public class LicenseRepo : ILicense
    {
        private readonly MyContext db;

        public LicenseRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(License obj)
        {
            await db.Licenses.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        

        public async Task DeleteAsync(License obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }


        //******************
        public async Task<IEnumerable<License>> GetAllAsync()
        {
            var data = await db.Licenses
               .Include("Project") 
             
               .ToListAsync();

            return data;

        }
        public async Task<IEnumerable<License>> GetAllAsync(IEnumerable<Project> projects)
        {
            var data = new List<License>();
            foreach (var pi in projects)
            {
                data = await db.Licenses
                            .Where(p => p.ProjectId == pi.Id)
                        
                            .ToListAsync();
            }

            return data;
        }
        public async Task<License> GetByIdAsync(int Id)
        {
            var data = await db.Licenses
                        .Include(x => x.Project)  
                        .Where(a => a.Id == Id)
                        .OrderBy(x => x.Id) // يجب الترتيب أولاً
                        .LastOrDefaultAsync();
            return data;
        }
        public async Task<License?> GetByProjectIdAsync(int Id)
        {
            return await db.Licenses
                            .Include("Project")
                           //.Include(x => x.Project)
                           .OrderBy(l => l.Id)
                           .LastOrDefaultAsync(x => x.ProjectId == Id);
        }


        //******************
        public async Task<IEnumerable<License>> GetExpiredLicensesAsync()
        {
           var data = await db.Licenses
          .Include(a => a.Project) 
          .Where(p => p.ExpireDate < DateTime.Now) 
          .ToListAsync();

            return data;
        }

        public Task UpdateAsync(License obj)
        {
            throw new NotImplementedException();
        }
    }
}
