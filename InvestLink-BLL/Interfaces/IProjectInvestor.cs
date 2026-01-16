using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface IProjectInvestor
    {
        Task<IEnumerable<ProjectInvestor>> GetAllAsync(int invetorId);
        Task CreateAsync(ProjectInvestor obj);
        Task<ProjectInvestor> GetByIdAsync(int Id);
        Task UpdateAsync(ProjectInvestor obj);
        Task DeleteAsync(ProjectInvestor obj);
    }
}
