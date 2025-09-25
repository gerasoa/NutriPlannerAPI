using CCRS.Business.Models;

namespace CCRS.Business.Interfaces
{
    public interface IConsultationConfigRepository : IRepository<ConsultationConfig> 
    {
        Task<IEnumerable<ConsultationConfig>> GetAllAsync(DateTime startDate, int daysAhead);
    }
}
