using AutoMapper;
using CCRS.Api.ViewModels;
using CCRS.Business.Models;

namespace CCRS.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Doctor, DoctorViewModel>().ReverseMap();
            CreateMap<Patient, PatientViewModel>().ReverseMap();
            CreateMap<Patient, PatientAddressViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
