using CCRS.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Interfaces
{
    public interface IConsultationConfigService : IDisposable
    {
        Task<ConsultationConfig> GetByIdAsync(Guid id);
        Task<IEnumerable<ConsultationConfig>> GetAllAsync(DateTime startDate, int daysAhead);
        Task AddAsync(ConsultationConfig config);
        Task UpdateAsync(ConsultationConfig config);
        Task DeleteAsync(Guid id);
    }
}
