using CCRS.Business.Models;
using CCRS.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Interfaces
{
    public interface IPatientService : IDisposable
    {
        Task<Patient> Add(Patient patient);
        Task<bool> Update(Patient patient);
        Task<bool> DeleteAsync(Guid id);
        Task<Address> AddAddress(Address address);
        Task<bool> UpdateAddress(Address address);       
    }
}
