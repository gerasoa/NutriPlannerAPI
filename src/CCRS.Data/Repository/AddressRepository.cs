using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CCRS.Data.Repository
{
    public class AddressRepository : Repository<Address> , IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Address> GetAddressByPatient(Guid patientId)
        {
            return await Db.Addresses.AsNoTracking()
                 .FirstOrDefaultAsync(f => f.PatientId == patientId);
        }       
    }
}
