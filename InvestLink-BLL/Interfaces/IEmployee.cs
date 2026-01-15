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
        Task<IEnumerable<Employee>> GetAllAsync();
        Task CreateAsync(Employee obj);
        int GetIdByEmail(string Email);
        Task<Employee> GetByEmailAsync(string email);
        Task<Employee> GetByIdAsync(int Id);
        Task UpdateAsync(Employee obj);
        Task DeleteAsync(Employee obj);
    }
}
