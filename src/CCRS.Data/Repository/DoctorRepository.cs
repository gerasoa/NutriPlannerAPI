using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Data.Context;

namespace CCRS.Data.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
