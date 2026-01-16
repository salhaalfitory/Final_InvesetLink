using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface IEmployee
    {
        ///---------------------------
        Task<Employee> GetByEmailAsync(string Email);
        int GetIdByEmail(string Email);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task CreateAsync(Employee obj);
        Task<Employee> GetByIdAsync(int Id);
        Task UpdateAsync(Employee obj);
        Task DeleteAsync(Employee obj);
    }
}
