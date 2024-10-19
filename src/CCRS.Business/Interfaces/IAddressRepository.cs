using CCRS.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByPatient(Guid patientId);
    }
}
