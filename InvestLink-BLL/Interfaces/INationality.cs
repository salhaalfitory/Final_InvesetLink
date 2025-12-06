using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface INationality
    {
        Task<IEnumerable<Nationality>> GetAllAsync();
        Task CreateAsync(Nationality obj);
        Task<Nationality> GetByIdAsync(int Id);
        Task UpdateAsync(Nationality obj);
        Task DeleteAsync(Nationality obj);
    }
}
