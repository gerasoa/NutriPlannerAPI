using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Data.Context;

namespace CCRS.Data.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext db) : base(db)
        {
        }
    }
}
