using CCRS.Business.Models;
using CCRS.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Interfaces
{
    public interface IAppointmentService : IDisposable
    {
        Task<Appointment> Add(Appointment appointment);

        //Task<bool> Update(Patient patient);
        
        //Task<bool> DeleteAsync(Guid id);

        //Task<bool> UpdateAddress(Address address);       
    }
}
