using InvestLink_DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface IProjectCoordinator
    {
        Task<IEnumerable<ProjectCoordinator>> GetAllAsync();
        Task<IEnumerable<ProjectCoordinator>> GetAllAsync(int emp);

        //Task<IEnumerable<ProjectCoordinator>> GetAllAsync(ProjectCoordinator obj);
        Task CreateAsync(ProjectCoordinator obj);
        Task<ProjectCoordinator> GetByIdAsync(int Id);
        Task<ProjectCoordinator> GetByIdAsync(int ProjectId, int EmployeeId);

        Task UpdateAsync(ProjectCoordinator obj);
        Task DeleteAsync(ProjectCoordinator obj);

    }
}
