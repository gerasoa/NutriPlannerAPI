using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CCRS.Data.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Patient>> GetAllPatientsByDoctor(Guid doctorId)
        {
            return await Db.Patients.AsNoTracking()
                .Include(a => a.Address)
                .Where(d => d.DoctorId == doctorId)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        //public async Task<Patient> GetPatientsById(Guid patientId)
        //{
        //    return await Db.Patients.Where(d => d.Id == patientId);
        //}

        public async Task<Patient> GetPatientAddressById(Guid patientId)
        {
            return await Db.Patients.AsNoTracking()
                .Include(a => a.Address)
                .Where(d => d.Id == patientId).FirstAsync();
        }

       
    }
}
