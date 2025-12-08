using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface IProjectFollowUp
    {
        Task<IEnumerable<ProjectFollowUp>> GetAllAsync();
        Task CreateAsync(ProjectFollowUp obj);
        Task<ProjectFollowUp> GetByIdAsync(int Id);
        Task UpdateAsync(ProjectFollowUp obj);
        Task DeleteAsync(ProjectFollowUp obj);
    }
}
