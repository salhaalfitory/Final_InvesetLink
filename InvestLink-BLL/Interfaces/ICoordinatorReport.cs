using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
   public interface ICoordinatorReport
    {
        Task<IEnumerable<CoordinatorReport>> GetAllAsync();
        Task CreateAsync(CoordinatorReport obj);
        Task<CoordinatorReport> GetByIdAsync(int Id);
        Task<IEnumerable<CoordinatorReport>> GetAllAsync(int iEmptId);
        Task<IEnumerable<CoordinatorReport>> GetAllAsync(IEnumerable<CoordinatorReport> iicoordinatorReport);
        Task UpdateAsync(CoordinatorReport obj);
        Task DeleteAsync(CoordinatorReport obj);
    }
}
