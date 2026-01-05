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
    public class AdvertisementRepo : IAdvertisement
    {
        private readonly MyContext db;

        public AdvertisementRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(Advertisement obj)
        {
            await db.Advertisements.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Advertisement obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            var data = await db.Advertisements.ToListAsync();
            return data;
        }

        public async Task<Advertisement> GetByIdAsync(int Id)
        {
            return await db.Advertisements
                       .Include(x => x.Employee)     
                             
                       .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public  async Task UpdateAsync(Advertisement obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
