using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface IEmployeeProject
    {
        Task<IEnumerable<EmployeeProject>> GetAllAsync();
        Task CreateAsync(EmployeeProject obj);
        Task<EmployeeProject> GetByIdAsync(int Id);
        Task UpdateAsync(EmployeeProject obj);
        Task DeleteAsync(EmployeeProject obj);
    }
}
