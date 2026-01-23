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
    public class EmployeeRepo : IEmployee
    {
        private readonly MyContext db;

        public EmployeeRepo(MyContext db)
        {
            this.db = db;
        }
        public async  Task CreateAsync(Employee obj)
        {
            await db.Employees.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var data = await db.Employees.ToListAsync();
            return data;
        }
        public int GetIdByEmail(string Email)
        {
            var data = db.Employees.FirstOrDefault(e => e.Email == Email);
            if (data == null)
            {
                return 0; // عدم العثور على الموظف
            }
            else
            {
                return data.Id;
            }
        }
        public async Task<Employee> GetByEmailAsync(string email)
        {
            var data = await db.Employees.Where(a => a.Email == email).FirstOrDefaultAsync();
            return data;
        }
        public async Task<Employee> GetByIdAsync(int Id)
        {
            var data = await db.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            return data;
        }

        public async Task UpdateAsync(Employee obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByEmailsAsync(List<string> emails)
        {
            var foundEmployees = new List<Employee>();

         
            if (emails == null) return foundEmployees;

            // بحث على كل إيميل 
            foreach (var email in emails)
            {
                // البحث عن موظف واحد في كل مرة
                var emp = await db.Employees.FirstOrDefaultAsync(e => e.Email == email);
                if (emp != null)
                {
                    foundEmployees.Add(emp);
                }
            }

            return foundEmployees;
        }
    }
}
