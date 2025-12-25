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
    public class InvestorRepo : IInvestor
    {
        private readonly MyContext db;

        public InvestorRepo(MyContext db)
        {
            this.db = db;
        }
        public  async Task CreateAsync(Investor obj)
        {       
            await db.Investors.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Investor obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Investor>> GetAllAsync()
        {
            var data = await db.Investors.ToListAsync();
            return data;
        }

        public async Task<Investor> GetByIdAsync(int Id)
        {
            var data = await db.Investors.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public async Task UpdateAsync(Investor obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
