using InvestLink_BLL.Interfaces;
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
    public class ProjectRepo : IProject
    {
        private readonly MyContext db;

        public ProjectRepo(MyContext db)
        {
            this.db = db;
        }
        public async Task<int> CreateAsync(Project obj)
        {
            db.Projects.Add(obj);
            await db.SaveChangesAsync();
            return obj.Id;
        }


        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await db.Projects
                           .Include(p => p.Licenses)
                           .ToListAsync();
        }

       public async Task<IEnumerable<Project>> GetAllAsync(IEnumerable<ProjectInvestor> projectInvestors)
        {
            List<Project> data = new List<Project>();
            if (projectInvestors != null)
            {
                // 3. الدوران وجلب البيانات
                foreach (ProjectInvestor projectInvestor in projectInvestors)
                {
                    // يبحث عن المشروع بالـ ID ويضيفه للقائمة
                    // لاحظ استخدام await لأن FindAsync عملية غير متزامنة
                    data.Add(await db.Projects.FindAsync(projectInvestor.ProjectId));
                }
            }
            return data;
        }


        //public async Task<IEnumerable<Project>> GetAllAsync(IEnumerable<ProjectInvestor> projectInvestors)
        //{
        //    var data = new List<Project>();

        //    if(projectInvestors != null)
        //    {
        //        foreach (var pr in projectInvestors)
        //        {
        //            data = await db.Projects
        //                .Where(p => p.Id == pr.ProjectId)
        //                .Include(p => p.Licenses)
        //                .FirstOrDefaultAsync();    
        //        }
        //        return data;
        //    }
        //}
        //var data = new List<Project>();
        //foreach (var pi in projectInvestors)
        //{
        //    data = await db.Projects
        //                .Where(p => p.Id == pi.ProjectId)
        //                .ToListAsync();
        //}



        //public async Task<IEnumerable<Project>> GetAllAsync(IEnumerable<ProjectInvestor> projectInvestors)
        //{
        //    var data = new List<Project>();
        //    foreach (var pi in projectInvestors)
        //    {
        //       data = await db.Projects
        //                   .Where(p => p.Id == pi.ProjectId)
        //                   .ToListAsync();
        //    }

        //    return data;
        //}

        public async Task<Project> GetByIdAsync(int Id)
        {
            var data = await db.Projects.Where(a => a.Id == Id)
               .Include(a => a.ProjectInvestors)       // 1. جدول الربط
               .ThenInclude(pi => pi.Investor)     // 2. المستثمر
               .ThenInclude(i => i.Nationality)

                .FirstOrDefaultAsync();
            return data;
        }

        
        public async Task<IEnumerable<Project>> GetByStateAsync(string state)
        {
            var data = await db.Projects
            .Where(p => p.State == state)
            .ToListAsync();
            return data;
        }



       

        public async Task UpdateAsync(Project obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        
    }
}
