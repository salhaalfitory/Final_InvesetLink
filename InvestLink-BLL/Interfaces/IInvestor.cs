using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface IInvestor
    {
        Task<IEnumerable<Investor>> GetAllAsync();
        Task<int> CreateAsync(Investor obj);
        Task<Investor> GetByIdAsync( int Id);
        int GetIdByEmail(string email);

        Investor GetByEmailAsync(string Email);
        Task UpdateAsync(Investor obj);
        Task DeleteAsync(Investor obj);
       

    }
}
