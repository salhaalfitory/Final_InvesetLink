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
            var data = await db.Licenses.ToListAsync();
            return data;
        }

        public Task<License> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        //public async Task<License> GetByIdAsync(int Id)
        //{
        //    var data = await db.Licenses.Where(a => a.Id == Id)
        //        .Include("Project")
        //       .FirstOrDefaultAsync();
        //    return data;
        //}
        public async Task<License?> GetByProjectIdAsync(int Id)
        {
            return await db.Licenses
                            .Include("Project")
                           //.Include(x => x.Project)
                           .FirstOrDefaultAsync(x => x.Id == Id);
        }

        //public async Task<License?> GetByIdAsync(int id)
        //{
        //    return await db.Licenses
        //          .Include(x => x.Project) // هذا أفضل وأضمن من "Project"
        //          .FirstOrDefaultAsync(x => x.Id == id);
        //}
            //return await db.Licenses
            //               .Include("Project")
            //               .FirstOrDefaultAsync(x => x.Id == id);
        

        public async Task UpdateAsync(License obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }



       
    }
}
