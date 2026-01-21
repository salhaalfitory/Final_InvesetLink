using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
   public interface IProject
    {
        Task<int> CreateAsync(Project obj);
        Task UpdateAsync(Project obj);
        Task<IEnumerable<Project>> GetAllAsyncByFiltter(IEnumerable<ProjectInvestor> projectInvestors , string status);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int Id);


        Task<IEnumerable<Project>> GetByStateAsync(string state);

        Task<Project> GetBydAsync(int Id);

        Task<Project> GetByIdForEditAsync(int Id);


    }
}
