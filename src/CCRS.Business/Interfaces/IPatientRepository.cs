using CCRS.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetAllPatientsByDoctor(Guid doctorId);
        Task<Patient> GetPatientAddressById(Guid patientId);
    }
}
