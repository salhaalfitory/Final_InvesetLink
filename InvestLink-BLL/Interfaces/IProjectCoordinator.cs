using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface IProjectCoordinator
    {
        Task<IEnumerable<ProjectCoordinator>> GetAllAsync(int employeeId);
        Task CreateAsync(ProjectCoordinator obj);
        Task<ProjectCoordinator> GetByIdAsync(int ProjectId, int EmployeeId);
        Task<ProjectCoordinator> GetByProjectIdAsync(int ProjectId);
        Task<ProjectCoordinator> GetByIdAsync( int Id);
        Task UpdateAsync(ProjectCoordinator obj);
        Task DeleteAsync(ProjectCoordinator obj);

    }
}
