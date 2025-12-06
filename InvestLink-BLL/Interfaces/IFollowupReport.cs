using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
   public interface IFollowUpReport
    {
        Task<IEnumerable<FollowUpReport>> GetAllAsync();
        Task CreateAsync(FollowUpReport obj);
        Task<FollowUpReport> GetByIdAsync(int Id);
        Task UpdateAsync(FollowUpReport obj);
        Task DeleteAsync(FollowUpReport obj);
    }
}
