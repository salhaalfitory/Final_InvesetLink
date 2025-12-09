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
        Task UpdateAsync(CoordinatorReport obj);
        Task DeleteAsync(CoordinatorReport obj);
    }
}
