using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Interfaces
{
    public interface IAdvertisement
    {
        Task<IEnumerable<Advertisement>> GetAllAsync();
        Task CreateAsync(Advertisement obj);
        Task<Advertisement> GetByIdAsync(int Id);
        Task UpdateAsync(Advertisement obj);
        Task DeleteAsync(Advertisement obj);
    }
}
