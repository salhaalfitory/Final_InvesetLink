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

    public class NationalityRepo : INationality
    {
        private readonly MyContext db;

        public NationalityRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(Nationality obj)
        {
            await db.Nationalitys.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Nationality obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public  async Task<IEnumerable<Nationality>> GetAllAsync()
        {
            var data = await db.Nationalitys.ToListAsync();
            return data;
        }

        public async Task<Nationality> GetByIdAsync(int Id)
        {
            var data = await db.Nationalitys.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public async Task UpdateAsync(Nationality obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
