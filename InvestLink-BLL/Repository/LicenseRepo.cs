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

      

        public async Task<IEnumerable<License>> GetAllAsync()
        {
            var data = await db.Licenses
               .Include("Project") 
               .ToListAsync();

           //Extra
            foreach (var item in data)
            {
                if (DateTime.Now > item.ExpireDate && item.State == true)
                {
                    item.State = false;                                       
                }
            }
            await db.SaveChangesAsync();
            return data;

        }





        public async Task<License> GetByIdAsync(int Id)
        {
            var data = await db.Licenses
                        .Include(x => x.Project)  // هذا أضمن وأفضل من كتابة "Project"
                        .Where(a => a.Id == Id)
                        .FirstOrDefaultAsync();
            return data;
        }
        public async Task<License?> GetByProjectIdAsync(int Id)
        {
            return await db.Licenses
                            .Include("Project")
                           //.Include(x => x.Project)
                           .FirstOrDefaultAsync(x => x.ProjectId == Id);
        }

    
        public async Task<IEnumerable<License>> GetExpiredLicensesAsync()
        {
            var data = await db.Licenses
          .Include(a => a.Project) 
          .Where(a => a.State==false) 
          .ToListAsync();

            return data;
        }

        public async Task UpdateAsync(License obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }



       
    }
}
