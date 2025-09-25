using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CCRS.Data.Repository
{
    public class ConsultationConfigRepository : Repository<ConsultationConfig>, IConsultationConfigRepository
    {
        public ConsultationConfigRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ConsultationConfig>> GetAllAsync(DateTime startDate, int daysAhead)
        {
            var endDate = DateTime.UtcNow.AddDays(daysAhead);
            return await Db.ConsultationConfigs
                .Include(c => c.AvailableSlots)
                .Where(c => c.AvailableSlots.Any(slot => 
                                (slot.DayOfWeek >= startDate.DayOfWeek &&
                                 slot.DayOfWeek <= endDate.DayOfWeek)))
                .ToListAsync();
                                    
        }
    }
}
