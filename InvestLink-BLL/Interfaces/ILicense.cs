using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface ILicense
    {
        Task<IEnumerable<License>> GetAllAsync();
        Task CreateAsync(License obj);
        Task<License> GetByIdAsync(int Id);
        Task UpdateAsync(License obj);
        Task DeleteAsync(License obj);
        Task<License?> GetByProjectIdAsync(int Id);

    }
}
