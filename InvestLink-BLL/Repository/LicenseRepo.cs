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
            var data = await db.Licenses.ToListAsync();
            return data;
        }

        public async Task<License?> GetByIdAsync(int Id)
        {
            return await db.Licenses
                           .Include(x => x.Project)
                           .FirstOrDefaultAsync(x => x.Id == Id);
        }


        //}
        public async Task<License?> GetByProjectIdAsync(int Id)
        {
            return await db.Licenses
                            .Include("Project")
                           //.Include(x => x.Project)
                           .FirstOrDefaultAsync(x => x.ProjectId == Id);
        }



        public async Task UpdateAsync(License obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }



       
    }
}
